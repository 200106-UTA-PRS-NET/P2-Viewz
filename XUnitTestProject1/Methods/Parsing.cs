//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace XUnitTestProject1.Methods
//{
//    public static class Parsing
//    {
//        public static string ReplaceInside(string content, string to_replace, string replace_with)
//        {
//            string To_Replace = to_replace;
//            string Replace_With = replace_with;

//            int climber = 0;
//            int checkpoint = 0;
//            while (checkpoint < content.Length)
//            {
//                //climbing
//                climber = content.IndexOf(To_Replace, checkpoint);

//                //climber fell?
//                if (climber < checkpoint)
//                    break;

//                //climber reached new height
//                checkpoint = climber + To_Replace.Length;

//                //reset replacement strings
//                to_replace = To_Replace;
//                replace_with = Replace_With;

//                //get string within quotes
//                int i = checkpoint;
//                while (i < content.Length && content[i] != '"')
//                {
//                    to_replace += content[i];
//                    replace_with += content[i];
//                    i++;
//                }

//                //include end quote
//                if (i < content.Length)
//                {
//                    to_replace += content[i];
//                    replace_with += content[i];
//                }

//                //climber replacing rock
//                content = content.Replace(to_replace, replace_with);
//            }

//            return content;
//        }
//    }
//}
