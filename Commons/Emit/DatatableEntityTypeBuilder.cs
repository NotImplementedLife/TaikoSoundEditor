using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json.Serialization;
using TaikoSoundEditor.Commons.Extensions;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.Emit
{
    internal class DatatableEntityTypeBuilder
    {
        private readonly ModuleBuilder mb;        

        public DatatableEntityTypeBuilder()
        {
            var aName = new AssemblyName(DynamicAssemblyName);
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            mb = ab.DefineDynamicModule(aName.Name ?? DynamicAssemblyName);
        }

        public static Dictionary<string, Type> LoadTypes(DynamicTypeCollection dTypes)
        {
            var builder = new DatatableEntityTypeBuilder();
            var result = new Dictionary<string, Type>();
            foreach (var dtype in dTypes.Types)
            {
                var @interface = Types.GetTypeByName(dtype.Interface);
                result[dtype.Name] = builder.BuildType(dtype.Name, @interface, dtype.Properties.Select(_ => _.CreatePropertyInfo()));
            }
            return result;
        }

        public Type BuildType(string name, Type @interface, IEnumerable<EntityPropertyInfo> properties)
        {
            TypeBuilder tb = @interface == null
                ? mb.DefineType(name, TypeAttributes.Public)
                //: mb.DefineType(name, TypeAttributes.Public);
                : mb.DefineType(name, TypeAttributes.Public, null, new Type[] { @interface });
            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator ctorIL = ctor.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);            
            ctorIL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            properties.ForEach(GeneratePropertyAction(tb, ctorIL));
            ctorIL.Emit(OpCodes.Ret);                            


            if(@interface!=null)
            {
                Debug.WriteLine("ERE??");
                var recastedProps = @interface.GetProperties()
                    .Where(p => p.GetCustomAttribute<RecastAttribute>() != null)
                    .ToArray();
                Debug.WriteLine(recastedProps.Length);

                foreach(var prop in recastedProps)
                {                    
                    Debug.WriteLine($"Recasted : {prop} ");
                    GenerateRecastedProperty(tb, prop, prop.GetCustomAttribute<RecastAttribute>().PropertyName);
                }
            }

            var type = tb.CreateType();
            /*var methods = tb.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty);
            foreach (var m in methods)
                Debug.WriteLine(m);*/

            return type;
        }      

        private Action<EntityPropertyInfo> GeneratePropertyAction(TypeBuilder tb, ILGenerator ilg) => _ => GenerateProperty(tb, ilg, _);

        private static Expression GetProperty(Expression instance, Type classType, string name)
        {
            var getProperty = typeof(Type).GetMethods().Where(m => m.Name == "GetProperty" && m.GetParameters().Length == 1).First();
            var getValueMethod = typeof(PropertyInfo).GetMethods().Where(m => m.Name == "GetValue" && m.GetParameters().Length == 1).First();
            var propInfo = Expression.Call(Expression.Constant(classType), getProperty, Expression.Constant(name));
            Debug.WriteLine(propInfo);
            var call = Expression.Call(propInfo, getValueMethod, instance);
            Debug.WriteLine(call);
            return call;            
        }

        public static LambdaExpression ConvertGetExpr<T>(string castSourceName)
        {            
            var param = Expression.Parameter(typeof(object));
            var getType = typeof(object).GetMethods().Where(m => m.Name == "GetType" && m.GetParameters().Length == 0).First();
            var getProperty = typeof(Type).GetMethods().Where(m => m.Name == "GetProperty" && m.GetParameters().Length == 1).First();
            var getValue = typeof(PropertyInfo).GetMethods().Where(m => m.Name == "GetValue" && m.GetParameters().Length == 1).First();
            var getTypeExpr = Expression.Call(param, getType);
            var getPropertyExpr = Expression.Call(getTypeExpr, getProperty, Expression.Constant(castSourceName));
            var getValueExpr = Expression.Call(getPropertyExpr, getValue, param);
            return Expression.Lambda(Expression.Convert(getValueExpr, typeof(T)), param);
        }

        public static LambdaExpression ConvertSetExpr<T>(string castSourceName, PropertyInfo prop)
        {
            Debug.WriteLine("HERE?");
            Debug.WriteLine(prop);
            var param0 = Expression.Parameter(typeof(object));
            var param1 = Expression.Parameter(typeof(T));            
            var getType = typeof(object).GetMethods().Where(m => m.Name == "GetType" && m.GetParameters().Length == 0).First();
            var getProperty = typeof(Type).GetMethods().Where(m => m.Name == "GetProperty" && m.GetParameters().Length == 1).First();
            var setValue = typeof(PropertyInfo).GetMethods().Where(m => m.Name == "SetValue" && m.GetParameters().Length == 2).First();
            var changeType = typeof(Convert).GetMethods().Where(m => m.Name == "ChangeType" && m.GetParameters().Length == 2
                && m.GetParameters()[1].ParameterType == typeof(Type)).First();
            

            var getTypeExpr = Expression.Call(param0, getType);
            var getPropertyExpr = Expression.Call(getTypeExpr, getProperty, Expression.Constant(castSourceName));
            var propertyTypeExpr = Expression.Property(getPropertyExpr, "PropertyType");
            var converted = Expression.Convert(param1, typeof(object));
            var changeTypeExpr = Expression.Call(changeType, converted, propertyTypeExpr);
            Debug.WriteLine(getPropertyExpr);
            Debug.WriteLine(changeTypeExpr);
            var setValueExpr = Expression.Call(getPropertyExpr, setValue, param0, changeTypeExpr);            
            Debug.WriteLine(setValueExpr);

            var returnLabel = Expression.Label(typeof(void));
            return Expression.Lambda(Expression
                .Block(setValueExpr, Expression.Return(returnLabel), Expression.Label(returnLabel)), param0, param1);
        }

        static MethodInfo ConvertGetExprMethod(Type t) => 
            typeof(DatatableEntityTypeBuilder).GetMethod("ConvertGetExpr", BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(t);

        static MethodInfo ConvertSetExprMethod(Type t) =>
            typeof(DatatableEntityTypeBuilder).GetMethod("ConvertSetExpr", BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(t);

        private void GenerateRecastedProperty(TypeBuilder tb, PropertyInfo prop, string castSourceName)
        {            
            PropertyBuilder pb = tb.DefineProperty(prop.Name, PropertyAttributes.HasDefault, prop.PropertyType, null);

            if (prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                pb.SetCustomAttribute(new CustomAttributeBuilder(typeof(JsonIgnoreAttribute).GetConstructor(new Type[0]), new object[0]));
            
            MethodBuilder mbGetAccessor = tb.DefineMethod($"get_{prop.Name}", GetSetAttr, prop.PropertyType, Type.EmptyTypes);
            var cvGetExpr = ConvertGetExprMethod(prop.PropertyType).Invoke(null, new object[] { castSourceName }) as LambdaExpression;
            var m = tb.DefineMethod($"RecastGet{prop.Name}", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard,
                prop.PropertyType, new Type[] { typeof(object) });
            Debug.WriteLine(cvGetExpr);
            cvGetExpr.CompileToMethod(m);            

            var il = mbGetAccessor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, m);
            il.Emit(OpCodes.Ret);

            pb.SetGetMethod(mbGetAccessor);

            MethodBuilder mbSetAccessor = tb.DefineMethod($"set_{prop.Name}", GetSetAttr, null, new Type[] { prop.PropertyType });
            var cvSetExpr = ConvertSetExprMethod(prop.PropertyType).Invoke(null, new object[] { castSourceName, prop }) as LambdaExpression;
            m = tb.DefineMethod($"RecastSet{prop.Name}", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard,
                typeof(void), new Type[] { typeof(object), prop.PropertyType });
            cvSetExpr.CompileToMethod(m);

            il = mbSetAccessor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, m);
            il.Emit(OpCodes.Ret);
          
            pb.SetSetMethod(mbSetAccessor);
        }

        private void GenerateProperty(TypeBuilder tb, ILGenerator ilg, EntityPropertyInfo property)
        {
            FieldBuilder fb = tb.DefineField($"m_{property.Name}", property.Type, FieldAttributes.Private);

            if (property.DefaultValue != null) 
            {
                var vType = property.DefaultValue.GetType();
                var returnLabel = Expression.Label(vType);
                var expr = Expression.Lambda(
                    Expression.Block(
                        Expression.Return(returnLabel, Expression.Constant(property.DefaultValue)),
                        Expression.Label(returnLabel, Expression.Constant(property.DefaultValue))
                        )                    
                    );
                var m = tb.DefineMethod($"Initialize{property.Name}", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard,
                    vType, Type.EmptyTypes);
                expr.CompileToMethod(m);

                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Call, m);                
                ilg.Emit(OpCodes.Stfld, fb);
            }
            PropertyBuilder pb = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.Type, null);

            MethodBuilder mbGetAccessor = tb.DefineMethod($"get_{property.Name}", GetSetAttr, property.Type, Type.EmptyTypes);            
            ILGenerator getIL = mbGetAccessor.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fb);
            getIL.Emit(OpCodes.Ret);

            MethodBuilder mbSetAccessor = tb.DefineMethod($"set_{property.Name}", GetSetAttr, null, new Type[] { property.Type });
            ILGenerator mbSetIL = mbSetAccessor.GetILGenerator();            
            mbSetIL.Emit(OpCodes.Ldarg_0);
            mbSetIL.Emit(OpCodes.Ldarg_1);
            mbSetIL.Emit(OpCodes.Stfld, fb);
            mbSetIL.Emit(OpCodes.Ret);

            pb.SetGetMethod(mbGetAccessor);
            pb.SetSetMethod(mbSetAccessor);

            if (property.JsonPropertyName != null)
                AddAttribute(pb, typeof(JsonPropertyNameAttribute), property.JsonPropertyName);
            else
                AddAttribute(pb, typeof(JsonIgnoreAttribute), new object[0]);

            if (property.IsReadOnly)
                AddAttribute(pb, typeof(ReadOnlyAttribute), true);
        }

        private static void AddAttribute(PropertyBuilder pb, Type attributeType, params object[] args)
        {
            var ctor = (from c in attributeType.GetConstructors()
                        let pms = c.GetParameters()
                        where pms.Length == args.Length
                        where pms.Select(_ => _.ParameterType).Zip(args, (p, a) =>
                            a == null ? p.IsClass : p.IsAssignableFrom(a.GetType())).All(_ => _)
                        select c).FirstOrDefault();
            Debug.WriteLine(ctor?.ToString() ?? "ctor null");
            pb.SetCustomAttribute(new CustomAttributeBuilder(ctor, args));
        }

        private static readonly MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual;
        private static readonly string DynamicAssemblyName = "TSEDynEntities";

    }
}
