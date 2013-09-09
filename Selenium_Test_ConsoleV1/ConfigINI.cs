using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Selenium_Test_ConsoleV1
{
    class ConfigINI
    {
        private static String iniPath;
        private static List<string> nameList = new List<string>();
        private static List<string> valueList = new List<string>();

        public ConfigINI(String CurrentINIPath)
        {
            iniPath = CurrentINIPath;
            LoadINI(iniPath);
        }

        public static void Load(string CurrentINIPath)
        {
            iniPath = CurrentINIPath;
            LoadINI(iniPath);
        }

        private static void LoadINI(String FilePath)
        {
            //Dim FileStream As FileStream, SR As StreamReader
            String TempLine;
            Boolean strRead = false;

            int counter = 0;
            FileStream FS = new FileStream(FilePath, FileMode.Open);
            StreamReader SR = new StreamReader(FS);

            do
            {
                counter += 1;
                TempLine = SR.ReadLine();
                if ((counter == 1) && (TempLine == "[Gallup]"))
                {
                    strRead = true;
                }
                else if (strRead == true)
                {
                    if (!(TempLine == ""))
                    {
                        nameList.Add(GetSingleID(TempLine));
                        valueList.Add(GetSingleValue2(TempLine, 1));
                    }
                }
            } while (SR.Peek() >= 0);
            FS.Close();
        }

        private static string GetSingleID(string TempLine)
        {
            for (int x = 1;x< TempLine.Length;x++)
            {
                if (TempLine.Substring(x, 1) == "=")
                {
                    return TempLine.Substring(0, x);
                }
            }
            return "";
        }

        private static string GetSingleValue2(string TempLine, int ValuePosition)
        {
            int eqlPosition = TempLine.IndexOf("=");
            if (eqlPosition > -1)
            {
                return TempLine.Substring(eqlPosition + 1);
            }
            return "";
        }

        private static string GetSingleValue(string TempLine, int ValuePosition)
        {
            int StartValue;
            int ValueCounter = 1;
            Boolean FirstEqualsFlag = false;
            for (int x = 1; x < TempLine.Length;x++ )
            {
                if (TempLine.Substring(x, 1) == "=" && FirstEqualsFlag == false)
                {
                    FirstEqualsFlag = true;
                    StartValue = x + 1;
                    for (int y = x + 1; y < TempLine.Length; y++)
                    {
                        if (TempLine.Substring(y, 1) == "#")
                        {
                            if (ValueCounter == ValuePosition)
                            {
                                return TempLine.Substring(StartValue, y - StartValue);
                            }
                            StartValue = y + 1;
                            ValueCounter = ValueCounter + 1;
                        }
                        else if (y == TempLine.Length)
                        {
                            if (ValueCounter == ValuePosition)
                            {
                                return TempLine.Substring(StartValue, y - x);
                            }
                        }
                    }
                }
            }
            return "";
        }

        public static string GetValue(String ConfigID)
        {
            int x = nameList.IndexOf(ConfigID);
            {
                if (x > -1)
                {
                    return valueList[x];
                }
            }
            return "";
        }

        public static string GetPath()
        {
            //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
