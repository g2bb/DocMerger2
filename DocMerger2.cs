using System;
using System.IO;
using System.Collections.Generic;

namespace DocumentMerger2 {
    class DocMerger2 {
        static void Main(string[] args) {
            if (args.Length < 3) {
                Console.WriteLine("DocumentMerger2 <input_file_1> <input_file_2> ... <input_file_n> <output_file>");
                Console.WriteLine("Supply a list of text files to merge followed by the name of the resulting merged text file as command line arguments.");
            } else {
                Console.WriteLine("Document Merger 2");
                List<string> files = cleanNames(new List<string>(args).GetRange(0, args.Length - 1));
                string mergedName = cleanName(args[args.Length - 1]);
                try {
                    int charNum = mergeFiles(files, mergedName);
                    Console.WriteLine("{0} was successfully saved. The document contains {1} characters.", mergedName, charNum);
                } catch (Exception error) {
                    Console.WriteLine(error.Message);
                }
            }
        }

        static List<string> cleanNames(List<string> names) {
            List<string> clean = new List<string>();
            names.ForEach(name => clean.Add(cleanName(name)));
            return clean;
        }

        static string cleanName(string name) {
            if (Path.HasExtension(name)) {
                return name;
            } else {
                return name + ".txt";
            }
        }

        static int mergeFiles(List<string> names, string saveName) {
            StreamWriter sw = null;
            int charNum = 0;
            try {
                sw = new StreamWriter(saveName);
                foreach (string name in names) {
                    charNum += WriteLines(ReadText(name), sw);
                }
            } finally {
                if (sw != null) {
                    sw.Close();
                }
            }
            return charNum;
        }

        static List<string> ReadText(string fileName) {
            List<string> lines = new List<string>();
            StreamReader sr = null;
            try {
                sr = new StreamReader(fileName);
                string line = sr.ReadLine();
                while (line != null) {
                    lines.Add(line);
                    line = sr.ReadLine();
                }
                return lines;
            } finally {
                if (sr != null) {
                    sr.Close();
                }
            }
        }

        static int WriteLines(List<string> lines, StreamWriter sw) {
            int charNum = 0;
            foreach (string line in lines) {
                charNum += line.Length;
                sw.WriteLine(line);
            }
            return charNum;
        }
    }
}
