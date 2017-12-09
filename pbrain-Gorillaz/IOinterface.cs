using System;
using System.Collections.Generic;
using System.Text;

namespace Gorillaz
{
    class IOinterface
    {
        string input;
        
        /// <summary>
        /// Write a statement followed by a message on the standard output
        /// </summary>
        /// <param name="str"></param>
        public void SetOutput(string statement, dynamic str)
        {
            Console.WriteLine(statement + " " + str);
        }

        /// <summary>
        /// Write a message on the standard output
        /// </summary>
        /// <param name="str"></param>
        public void SetOutput(dynamic str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// Write a message on the error output
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="str"></param>
        public void SetError(string statement, dynamic str)
        {
            Console.Error.WriteLine(statement + " " + str);
        }

        /// <summary>
        /// Write a statement followed by a message on the error output
        /// </summary>
        /// <param name="str"></param>
        public void SetError(dynamic str)
        {
            Console.Error.WriteLine(str);
        }

        /// <summary>
        /// Read on the stadard input until a line matching keyWord, add the content to 
        /// the given string array and return it
        /// </summary>
        /// <param name="wordTab"></param>
        /// <returns></returns>
        string[] ReadUntil(string[] wordTab, string keyWord)
        {
            string input;
            string[] tmp;
            int arrayOriginalLength;

            while ((input = Console.ReadLine()) != keyWord)
            {
                tmp = input.Split(' ', ',');
                arrayOriginalLength = wordTab.Length;
                Array.Resize<string>(ref wordTab, arrayOriginalLength + tmp.Length);
                Array.Copy(tmp, 0, wordTab, arrayOriginalLength, tmp.Length);
            }
            return (wordTab);
        }

        /// <summary>
        /// fill the Info class
        /// </summary>
        /// <param name="infos"></param>
        void SetInfos(Infos infos)
        {

        }

        /// <summary>
        /// read the input on the standard input
        /// return 0 => nothing to do , 1 => player turn, 84 => error , 42 terminate the brain
        /// </summary>
        /// <param name="board"></param>
        public int GetInput(ref Board board, ref Infos infos)
        {
            input = Console.ReadLine();
            string[] wordTab = input.Split(' ', ',');
            int ret = -1;

            if (wordTab.Length > 0)
            {
                try
                {
                    switch (wordTab[0])
                    {
                        case "START":
                            if (wordTab.Length < 2 || board.SetBoardSize(Convert.ToInt32(wordTab[1])) == 1)
                                SetOutput("ERROR", "- Invalid board size, it mmust be between 5 and 40");
                            else
                                SetOutput("OK", "- parameters are good");
                            break;
                        case "TURN":
                            if (wordTab.Length < 3 ||
                                board.PlaceARock(2, Convert.ToInt32(wordTab[1]), Convert.ToInt32(wordTab[2])) == 1)
                                SetOutput("ERROR", " - Invalid placement");
                            else
                                ret = 1;
                            break;
                        case "BEGIN":
                            ret = 1;
                            break;
                        case "BOARD":
                            wordTab = ReadUntil(wordTab, "DONE");
                            board.FillFromWordTab(wordTab);
                            ret = 1;
                            break;
                        case "INFO":
                            SetInfos(infos);
                            ret = 0;
                            break;
                        case "END":
                            ret = 42;
                            break;
                        case "ABOUT":
                            SetOutput("name=\"Gorillaz\", version=\"1.0\", author\"Vigner Gaëtan / Lauga-cami Tom\", country=\"France\"");
                            break;
                        default:
                            SetOutput("Unknown", "- Invalid statement please check the protocole");
                            ret = 0;
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    SetError("ERROR", ex);
                    ret = 0;
                }
                catch (OverflowException ex)
                {
                    SetError("ERROR", ex);
                    ret = 0;
                }
            }
            return (ret);
        }
    }
}
