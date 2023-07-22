using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaikoSoundEditor.Extensions;
using TaikoSoundEditor.Utils;
using static TaikoSoundEditor.TJA;

namespace TaikoSoundEditor
{
    internal class TJA
    {
        public TJA(string[] content)
        {
            Parse(content);
        }

        public class Line
        {
            public string Type { get; set; }
            public string Scope { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }

            public Line(string type, string scope, string name, string value)
            {
                Type = type;
                Scope = scope;
                Name = name;
                Value = value;
            }
        }


        static readonly string[] HEADER_GLOBAL = new string[] 
        {
            "TITLE", "TITLEJA", "SUBTITLE", "BPM", "WAVE", "OFFSET", "DEMOSTART","GENRE",
        };

        static readonly string[] HEADER_COURSE = new string[]
        {
            "COURSE", "LEVEL", "BALLOON", "SCOREINIT", "SCOREDIFF", "TTROWBEAT",
        };

        static readonly string[] COMMAND = new string[]
        {
            "START","END","GOGOSTART","GOGOEND","MEASURE","SCROLL","BPMCHANGE","DELAY","BRANCHSTART","BRANCHEND","SECTION","N","E","M","LEVELHOLD","BMSCROLL","HBSCROLL","BARLINEOFF","BARLINEON","TTBREAK",
        };

        public Line ParseLine(string line)
        {
            Match match = null;

            Logger.Info($"Parsing line : {line}");            

            if ((match = line.Match("\\/\\/.*")) != null) 
            {               
                line = line.Substring(0, match.Index).Trim();
            }            

            if ((match = line.Match("^([A-Z]+):(.+)", "i")) != null)
            {
                var nameUpper = match.Groups[1].Value.ToUpper();
                var value = match.Groups[2].Value;
                Logger.Info($"Match = {nameUpper}, {value}");

                if (HEADER_GLOBAL.Contains(nameUpper))
                {
                    Logger.Info($"Detected header");
                    return new Line("header", "global", nameUpper, value.Trim());
                }
                else if (HEADER_COURSE.Contains(nameUpper))
                {
                    Logger.Info($"Detected course");
                    return new Line("header", "course", nameUpper, value.Trim());
                }
            }
            else if ((match = line.Match("^#([A-Z]+)(?:\\s+(.+))?", "i")) != null) 
            {
                var nameUpper = match.Groups[1].Value.ToUpper();
                var value = match.Groups[2].Value ?? "";

                if (COMMAND.Contains(nameUpper))
                {
                    Logger.Info($"Detected command");
                    return new Line("command", null, nameUpper, value.Trim());
                }
                else
                    Logger.Warning($"Unknown command : {nameUpper} with value {value.Trim()}");
            }
            else if ((match = line.Match("^(([0-9]|A|B|C|F|G)*,?)$")) != null) 
            {
                Logger.Info($"Detected command");
                var data = match.Groups[1].Value;
                return new Line("data", null, null, data);                
            }
            Logger.Warning($"Unknown line : {line}");
            return new Line("unknwon", null, null, line);
        }    
        

        public Course GetCourse(Header tjaHeaders, Line[] lines)
        {
            Logger.Info($"Getting course from {lines.Length} lines");
            var headers = new CourseHeader();

            var measures = new List<Measure>();
            var measureDividend = 4; 
            var measureDivisor = 4;
            var measureProperties = new Dictionary<string, bool>();
            var measureData = "";
            var measureEvents = new List<MeasureEvent>();
            var currentBranch = "N";
            var targetBranch = "N";
            var flagLevelhold = false;
            bool hasBranches = false;

            foreach(var line in lines)
            {
                if(line.Type=="header")
                {
                    Logger.Info($"header {line.Name} {line.Value}");

                    if (line.Name == "COURSE")
                        headers.Course = line.Value;
                    else if (line.Name == "LEVEL")
                        headers.Level = Number.ParseInt(line.Value);
                    else if (line.Name == "BALLOON")
                        headers.Balloon = new Regex("[^0-9]").Split(line.Value).Where(_ => _ != "").Select(Number.ParseInt).ToArray();
                    else if (line.Name == "SCOREINIT")
                        headers.ScoreInit = Number.ParseInt(line.Value);
                    else if (line.Name == "SCOREDIFF")
                        headers.ScoreDiff = Number.ParseInt(line.Value);
                    else if (line.Name == "TTROWBEAT")
                        headers.TTRowBeat = Number.ParseInt(line.Value);
                }
                else if(line.Type=="command")
                {
                    Logger.Info($"Command {line.Name} {line.Value}");

                    if (line.Name == "BRANCHSTART")
                    {
                        hasBranches = true;
                        if (!flagLevelhold)
                        {
                            var values = line.Value.Split(',');
                            if (values[0] == "r")
                            {
                                if (values.Length >= 3) targetBranch = "M";
                                else if (values.Length == 2) targetBranch = "E";
                                else targetBranch = "N";
                            }
                            else if (values[0] == "p")
                            {
                                if (values.Length >= 3 && Number.ParseFloat(values[2]) <= 100) targetBranch = "M";
                                else if (values.Length >= 2 && Number.ParseFloat(values[1]) <= 100) targetBranch = "E";
                                else targetBranch = "N";
                            }
                        }
                    }
                    else if (line.Name == "BRANCHEND")
                        currentBranch = targetBranch;
                    else if (line.Name == "N" || line.Name == "E" || line.Name == "M") 
                        currentBranch = line.Name;
                    else if(line.Name=="START" || line.Name == "END")
                    {
                        currentBranch = targetBranch = "N";                        
                        flagLevelhold = false;
                    }
                    else
                    {
                        if (currentBranch == targetBranch)
                        {
                            if (line.Name == "MEASURE")
                            {
                                var matchMeasure = line.Value.Match("(\\d+)\\/(\\d+)");
                                if (matchMeasure != null)
                                {
                                    measureDividend = Number.ParseInt(matchMeasure.Groups[1].Value);
                                    measureDivisor = Number.ParseInt(matchMeasure.Groups[2].Value);
                                }
                            }
                            else if (line.Name == "GOGOSTART")
                                measureEvents.Add(new MeasureEvent("gogoStart", measureData.Length));
                            else if (line.Name == "GOGOEND")
                                measureEvents.Add(new MeasureEvent("gogoEnd", measureData.Length));
                            else if (line.Name == "SCROLL")
                                measureEvents.Add(new MeasureEvent("scroll", measureData.Length, Number.ParseFloat(line.Value)));
                            else if (line.Name == "BPMCHANGE")
                                measureEvents.Add(new MeasureEvent("bpm", measureData.Length, Number.ParseFloat(line.Value)));
                            else if (line.Name == "TTBREAK")
                                measureProperties["ttBreak"] = true;
                            else if (line.Name == "LEVELHOLD")
                                flagLevelhold = true;

                        }
                    }

                }
                else if(line.Type=="data" && currentBranch==targetBranch)
                {
                    Logger.Info($"Data {line.Value}");
                    var data = line.Value;
                    if (data.EndsWith(","))
                    {
                        measureData += data.Substring(0, data.Length - 1);  //data.slice(0, -1);
                        var measure = new Measure(new int[] { measureDividend, measureDivisor }, measureProperties, measureData, measureEvents.ToList());
                        measures.Add(measure);
                        measureData = "";
                        measureEvents.Clear();
                        measureProperties.Clear();
                    }
                    else measureData += data;
                }
            } // foreach

            if(measures.Count>0)
            {
                // Make first BPM event
                var firstBPMEventFound = false;
                for (var i = 0; i < measures[0].Events.Count; i++) 
                {
                    var evt = measures[0].Events[i];

                    if (evt.Name == "bpm" && evt.Position == 0) 
                    {
                        firstBPMEventFound = true;
                        break;
                    }
                }

                if (!firstBPMEventFound)
                {
                    measures[0].Events = measures[0].Events.Prepend(new MeasureEvent("bmp", 0, tjaHeaders.Bpm)).ToList();
                }
            }

            // Helper values
            var course = 0;
            var courseValue = headers.Course.ToLower();

            if (courseValue == "easy" || courseValue == "0")
                course = 0;
            else if (courseValue == "normal" || courseValue == "1")
                course = 1;
            else if (courseValue == "hard" || courseValue == "2")
                course = 2;
            else if(courseValue=="oni" || courseValue=="3")
                course = 3;
            else if (courseValue == "edit" || courseValue == "ura"|| courseValue == "4")
                course = 4;

            Logger.Info($"Course difficulty =  {course}");

            if (measureData!="" || measureData!=null)
            {
                measures.Add(new Measure(new int[] { measureDividend, measureDivisor }, measureProperties, measureData, measureEvents));
            }
            else
            {
                foreach(var ev in measureEvents)
                {
                    ev.Position = measures[measures.Count - 1].MeasureData.Length;
                    measures[measures.Count - 1].Events.Add(ev);
                }
            }
            var c = new Course(course, headers, measures) { HasBranches = hasBranches };
            Logger.Info($"Course created : {c}");
            return c;
        }

        public void Parse(string[] lines)
        {
            Logger.Info($"Parse start");

            var headers = new Header();
            var courses = new Dictionary<int, Course>();

            int idx;
            var courseLines = new List<Line>();

            for(idx=0;idx<lines.Length;idx++)
            {
                var line = lines[idx];
                if (line == "") continue;
                var parsed = ParseLine(line);

                if(parsed.Type=="header" && parsed.Scope=="global")
                {
                    Logger.Info($"Header global {parsed.Name} = {parsed.Value}");

                    if (parsed.Name == "TITLE")
                        headers.Title = parsed.Value;
                    if (parsed.Name == "TITLEJA")
                        headers.TitleJa = parsed.Value;
                    if (parsed.Name == "SUBTITLE")
                        headers.Subtitle = parsed.Value.StartsWith("--") ? parsed.Value.Substring(2) : parsed.Value;
                    if (parsed.Name == "BPM")
                        headers.Bpm = Number.ParseFloat(parsed.Value);
                    if (parsed.Name == "WAVE")
                        headers.Wave = parsed.Value;
                    if (parsed.Name == "OFFSET")
                        headers.Offset = Number.ParseFloat(parsed.Value);
                    if (parsed.Name == "DEMOSTART")
                        headers.DemoStart = Number.ParseFloat(parsed.Value);
                    if (parsed.Name == "GENRE")
                        headers.Genre = parsed.Value;
                }
                else if (parsed.Type == "header" && parsed.Scope == "course")
                {
                    if (parsed.Name == "COURSE") 
                    {
                        Logger.Info($"Course found : {parsed.Value}");
                        if (courseLines.Count>0)
                        {
                            var course = GetCourse(headers, courseLines.ToArray());
                            courses[course.CourseN] = course;
                            courseLines.Clear();
                        }                        
                    }
                    courseLines.Add(parsed);
                }
                else
                    courseLines.Add(parsed);
            }

            if(courseLines.Count>0)
            {
                var course = GetCourse(headers, courseLines.ToArray());
                courses[course.CourseN] = course;
            }

            Headers = headers;
            Courses = courses;

            Logger.Info($"Parse end");
        }


        public Header Headers;
        public Dictionary<int, Course> Courses;

        public class Course
        {
            public int CourseN { get; set; }
            public CourseHeader Headers { get; set; }
            public List<Measure> Measures { get; set; }

            public bool HasBranches { get; set; }

            public Course(int courseN, CourseHeader headers, List<Measure> measures)
            {
                CourseN = courseN;
                Headers = headers;
                Measures = measures;
            }

            public override string ToString()
                => $"C({CourseN},{Headers},{string.Join("|",Measures)})";

            public ConvertedCourse Converted => ConvertToTimed(this);

            private static readonly List<string> typeNote = new List<string> { "don", "kat", "donBig", "katBig" };

            public int NotesCount
            {
                get
                {
                    Debug.WriteLine(string.Join("", Measures.Select(_ => _.MeasureData)));
                    return string.Join("", Measures.Select(_ => _.MeasureData)).Where(c => "1234".Contains(c)).Count();
                }
                //get => Converted.Notes.Where(n => typeNote.Contains(n.Type)).Count();            
            }
        }

        public class Measure
        {
            public int[] Length { get; set; }
            public Dictionary<string, bool> Properties { get; set; }
            public string MeasureData { get; set; }
            public List<MeasureEvent> Events { get; set; }

            public Measure(int[] length, Dictionary<string, bool> properties, string measureData, List<MeasureEvent> events)
            {
                Length = length;
                Properties = properties;
                MeasureData = measureData;
                Events = events;
            }

            public override string ToString() =>
                $"M({string.Join(" ", Length)},{string.Join(";", Properties.Select(kv => kv.Key + "=" + kv.Value))},{MeasureData},{string.Join(";", Events)})";
        }

        public class MeasureEvent
        {
            public string Name { get; set; }
            public int Position { get; set; }
            public float Value { get; set; }

            public MeasureEvent(string name, int position, float value = 0)
            {
                Name = name;
                Position = position;
                Value = value;
            }
            public override string ToString() => $"ME({Name},{Position},{Value}";
        }


        public class Header
        {
            public string Title { get; set; } = "";
            public string Subtitle { get; set; } = "";
            public string TitleJa { get; set; } = "";
            public float Bpm { get; set; } = 120;
            public string Wave { get; set; } = "";
            public float Offset { get; set; } = 0;
            public float DemoStart { get; set; } = 0;
            public string Genre { get; set; } = "";
            public override string ToString() => $"H({Title},{Subtitle},{Bpm},{Wave},{Offset},{DemoStart},{Genre})";
        }

        public class CourseHeader
        {
            public string Course { get; set; } = "Oni";
            public int Level { get; set; } = 0;
            public int[] Balloon { get; set; } = new int[0];
            public int ScoreInit { get; set; } = 100;
            public int ScoreDiff { get; set; } = 100;
            public int TTRowBeat { get; set; } = 16;

            public override string ToString() => $"CH({Course}, {Level}, {string.Join(";",Balloon)}, {ScoreInit}, {ScoreDiff}, {TTRowBeat})";            
        }

        public static List<byte[]> RunTja2Fumen(string sourcePath)
        {
            Logger.Info("Running tja2fumen");            
            sourcePath = Path.GetFullPath(sourcePath);
            Logger.Info($"source = {sourcePath}");

            var dir = Path.GetDirectoryName(sourcePath);
            var fname = Path.GetFileNameWithoutExtension(sourcePath);

            var p = new Process();
            p.StartInfo.FileName = Path.GetFullPath(@"Tools\tja2fumen.exe");
            p.StartInfo.ArgumentList.Add(sourcePath);            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.WaitForExit();
            int exitCode = p.ExitCode;
            string stdout = p.StandardOutput.ReadToEnd();
            string stderr = p.StandardError.ReadToEnd();

            if (exitCode != 0)
                throw new Exception($"Process tja2fumen failed with exit code {exitCode}:\n" + stderr);

            var result = new List<byte[]>
            {
                File.ReadAllBytes(Path.Combine(dir, fname + "_e.bin")),
                File.ReadAllBytes(Path.Combine(dir, fname + "_h.bin")),
                File.ReadAllBytes(Path.Combine(dir, fname + "_m.bin")),
                File.ReadAllBytes(Path.Combine(dir, fname + "_n.bin"))

            };

            File.Delete(Path.Combine(dir, fname + "_e.bin"));
            File.Delete(Path.Combine(dir, fname + "_h.bin"));
            File.Delete(Path.Combine(dir, fname + "_m.bin"));
            File.Delete(Path.Combine(dir, fname + "_n.bin"));
            
            if (File.Exists(Path.Combine(dir, fname + "_x.bin")))
            { 
                result.Add(File.ReadAllBytes(Path.Combine(dir, fname + "_x.bin")));
                File.Delete(Path.Combine(dir, fname + "_x.bin"));
            }            
            else
            {
                result.Add(new byte[0]);
            }

            for (int i = 1; i <= 2; i++)
            {
                int k = 0;
                foreach (var d in "ehmnx")
                {
                    var path = Path.Combine(dir, $"{fname}_{d}_{i}.bin");
                    if(File.Exists(path))
                    {
                        result.Add(File.ReadAllBytes(path));
                        File.Delete(path);
                    }
                    else
                    {
                        result.Add(result[k].ToArray());
                    }
                    k++;
                }                
            }

            return result;
        }

        public override string ToString() => $"{Headers}\n{string.Join("\n", Courses)}";

        

        public static ConvertedCourse ConvertToTimed(Course course)
        {
            List<TimedEvent> events=new List<TimedEvent>();
            List<Note> notes = new List<Note>();
            float beat = 0;
            int balloon=0;
            bool imo = false;

            //Debug.WriteLine("-----------------------------------------");

            for (int m = 0; m < course.Measures.Count; m++)
            {
                var measure = course.Measures[m];
                float length = measure.Length[0] / measure.Length[1] * 4;
                for (int e = 0; e < measure.Events.Count; e++)
                {
                    var evt = measure.Events[e];
                    float eBeat = length / (measure.MeasureData.Length == 0 ? 1 : measure.MeasureData.Length) * evt.Position;
                    if (evt.Name == "bpm")
                        events.Add(new TimedEvent("bmp", evt.Value, beat + eBeat));
                    else if (evt.Name == "gogoStart" || evt.Name == "gogoEnd")
                        events.Add(new TimedEvent(evt.Name, 0, beat + eBeat));                    
                }
                
                for(int d=0;d<measure.MeasureData.Length;d++)
                {
                    //Debug.WriteLine(measure.MeasureData[d]);
                    var ch = measure.MeasureData[d];
                    var nBeat = length / measure.MeasureData.Length * d;

                    var note = new Note("", beat + nBeat);
                    switch(ch)
                    {
                        case '1':
                            note.Type = "don";
                            break;

                        case '2':
                            note.Type = "kat";
                            break;

                        case '3':
                        case 'A':
                            note.Type = "donBig";
                            break;

                        case '4':
                        case 'B':
                            note.Type = "katBig";
                            break;

                        case '5':
                            note.Type = "renda";
                            break;

                        case '6':
                            note.Type = "rendaBig";
                            break;

                        case '7':
                            note.Type = "balloon";
                            note.Count = course.Headers.Balloon[balloon++];
                            break;

                        case '8':
                            note.Type = "end";
                            if (imo) imo = false;
                            break;

                        case '9':
                            if (imo == false)
                            {
                                note.Type = "balloon";
                                note.Count = course.Headers.Balloon[balloon++];
                                imo = true;
                            }
                            break;
                        default: break;
                    }
                    if (note.Type != "") notes.Add(note);
                }
                beat += length;
            }


            var times = PulseToTime(events, notes.Select(n => n.Beat).ToArray());

            for(int i=0;i<times.Length;i++)
            {
                notes[i].Time = times[i];
            }

            return new ConvertedCourse(course.Headers, events.ToArray(), notes.ToArray());
        }

        public class ConvertedCourse
        {
            public CourseHeader Headers { get; set; }
            public TimedEvent[] Events { get; set; }
            public Note[] Notes { get; set; }

            public ConvertedCourse(CourseHeader headers, TimedEvent[] events, Note[] notes)
            {
                Headers = headers;
                Events = events;
                Notes = notes;
            }
        }

        private static float[] PulseToTime(List<TimedEvent> events, float[] objects)
        {
            float bpm = 120;
            float passedBeat = 0;
            float passedTime = 0;
            int eidx = 0, oidx = 0;

            var times = new List<float>();
            while(oidx<objects.Length)
            {
                var evt = eidx < events.Count ? events[eidx] : null;
                var objBeat = objects[oidx];

                while (evt != null && evt.Beat <= objBeat)  
                {
                    if (evt.Type == "bpm") 
                    {
                        var _beat = evt.Beat - passedBeat;
                        var _time = 60 / bpm * _beat;

                        passedBeat += _beat;
                        passedTime += _time;
                        bpm = evt.Value;
                    }
                    eidx++;
                    evt = eidx < events.Count ? events[eidx] : null;
                }
                var beat = objBeat - passedBeat;
                var time = 60 / bpm * beat;
                times.Add(passedTime + time);

                passedBeat += beat;
                passedTime += time;
                oidx++;
            }
            return times.ToArray();
        }

        public class TimedEvent
        {
            public string Type { get; set; }
            public float Value { get; set; }
            public float Beat { get; set; }            

            public TimedEvent(string type, float value, float beat)
            {
                Type = type;
                Value = value;
                Beat = beat;
            }
        }

        public class Note
        {
            public string Type { get; set; }
            public float Beat { get; set; }
            public int Count { get; set; }
            public float Time { get; set; }

            public Note(string type, float beat)
            {
                Type = type;
                Beat = beat;
            }
        }

        public static TJA ReadAsUTF8(string path)
        {
            var lines = File.ReadAllLines(path);
            return new TJA(lines);
        }

        public static TJA ReadAsShiftJIS(string path)
        {
            var lines = File.ReadAllLines(path, Encoding.GetEncoding("shift_jis"));
            return new TJA(lines);
        }

        public static TJA ReadDefault(string path)
        {
            var bytes = File.ReadAllBytes(path).Take(3).ToArray();
            if (bytes[0]==0xEF && bytes[1]==0xBB && bytes[2]==0xBF) // BOM
            {
                return ReadAsUTF8(path);
            }
            return ReadAsShiftJIS(path);
        }

    }
}
