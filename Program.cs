using System;
using System.IO;
using System.Text;
using System.Threading;

namespace LRight {
    class Program {
        static bool
            REL = false
        ,   DEL = false
        ;
        #region Overloads
       /* ------------------------------
        * [REGION - Main Text Functions]
        * ------------------------------
        */// Ends At Line #88
        static void W(string s) {
            Console.WriteLine(s);
        }
        static bool NoDateText(int g) {
            if (g == 0x12 || g == 0x13 || g == 0x14 || g == 0x15)
                return true;
            else
                return false;
        }
        static StringBuilder blankspace(int num) {
            var s = new StringBuilder();
            try {
            s.Append(' ', Console.BufferWidth - num);
            }
            catch (Exception fyck){
                D($"|{fyck}\n|{num} {Console.BufferWidth}");
                R();
            }
            return s;
        }
        static void D(string s) {
            if (REL) return; 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        static void CW(string s, bool c) {
            switch (c) {
                case true:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(s);
                    Console.ResetColor();
                    break;
                case false:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(s);
                    Console.ResetColor();
                    break;
            }
        }
        static ConsoleKey R() { // faster to type, lul
            return Console.ReadKey(true).Key;
        }
        static string ReadLine(int line) {
            StringBuilder s = new StringBuilder();
            while (true) {
                var input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Enter) {
                    return $"{s}";
                }
                if (input.Key == ConsoleKey.Backspace) {
                    if (s.Length > 0) {
                        s.Remove(s.Length-1, 1);
                        Console.SetCursorPosition(0, line);
                        Console.Write($"{s}{blankspace(s.Length)}");
                        Console.SetCursorPosition(s.Length, line);
                    } else return "\n";
                }
                if (input.Key == ConsoleKey.Delete) {
                    s.Remove(0, s.Length);
                    Console.SetCursorPosition(0, line);
                    Console.Write($"{s}{blankspace(0)}");
                    Console.SetCursorPosition(0, line);
                }
                if (input.Key != ConsoleKey.Escape & input.Key != ConsoleKey.Enter & input.Key != ConsoleKey . Delete & input.Key != ConsoleKey.Backspace) {
                    s.Append(input.KeyChar);
                    Console.SetCursorPosition(0, line);
                    Console.Write($"{s}{blankspace(s.Length)}");
                    Console.SetCursorPosition(s.Length, line);
                } else if (input.Key.HasFlag(ConsoleKey.Escape)) {
                    return "";
                }
            }
        }
        static byte[] Time(int i) { // Fuck It, Good Enough.
            try {
                string date = $"{DateTime.Now.GetDateTimeFormats()[107]}";
                byte[] month = Encoding.ASCII.GetBytes(new char[] { date[3], date[4], date[5], ' ' });
                byte[] year = Encoding.ASCII.GetBytes(new char[] { date[7], date[8], date[9], date[10] });
                byte[] time = Encoding.ASCII.GetBytes(date.Substring(date.IndexOf(' '))); time[0] = 0x00;
                byte[] day = Encoding.ASCII.GetBytes((i <3 || i > 9 && i < 16) ? $" {DateTime.Now.Day} " : $"{DateTime.Now.Day} ");

                int L = 18 + day.Length, d = day.Length; byte[] DT = new byte[L];

                Buffer.BlockCopy(month, 0, DT, 0, 4);
                Buffer.BlockCopy(day, 0, DT, 4, d);
                Buffer.BlockCopy(year, 0, DT, 4+d, 4);
                Buffer.BlockCopy(time, 0, DT, (d == 2 ? 9 : 8) + d, 9);
                return DT;
            }
            catch (Exception tabarnack) {
                W(tabarnack.ToString()); R();
                return new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
            }
        }
        #endregion
        static void Main() {
            #region Decs
            /* -------------------------------------------------
             * [REGION - Main Functions And Vaiable Declerations]
             * -------------------------------------------------
             * || Ends At Line #359
             * 
             * --------------------
             * (SettingsWriteOrder)
             * --------------------
             *gameE
             *gameP
             *SetupTypeC
             *SetupTypeF
             *UCC
             *bls[] {
             *IncludeBuildTxtE
             *IncludeBuildTxtP
             *ShowWrittenBuild
             *CutoffCheck
             *Post-BuildCutoff
             *WriteDate&Time
             *}
             *DebTxt
             *?CE
             *?CE2
             *?CE3
             *?CP
             *buildi
             *len
             *path
             */
            string flt, path, tmpS = "tmp";
            string[] paths = new string[] { @"\eboot.bin", @"\big2-ps4_Shipping.elf", @"\big3-ps4_Shipping.elf" };

            int tmpI, len, CE = 0, CE2 = 0, CE3 = 0, TE = 0, CP = 0, buildi, Page, GameInd, SetInd, Game;

            byte gameE = 0xFF, gameP = 0xFF, SetupTypeC = 0x00, SetupTypeF = 0x00, DebTxt = 0x00;
            byte[] data, tmp = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };

            bool IsLastPage = false, UCC;

            bool[] bls = new bool[] { true, true, true, true, true, true };


            string PageNum() {
                return IsLastPage ? "1" : (Page+2).ToString();
            }
            void Viewettings() {
                Console.Clear();
                Console.CursorVisible = false;
                SetInd = 0;
                string[] onoff = new string[7];
top:
                Console.SetCursorPosition(0, 0);

                if (bls[0]) onoff[0] = "Yes"; else onoff[0] = "No";
                if (bls[1]) onoff[1] = "Yes"; else onoff[1] = "No";
                if (bls[2]) onoff[2] = "Yes"; else onoff[2] = "No";
                if (bls[3]) onoff[3] = "Yes"; else onoff[3] = "No";
                if (bls[4]) onoff[4] = "Yes"; else onoff[4] = "No";
                if (bls[5]) onoff[5] = "Yes"; else onoff[5] = "No";
                if (Game <27 & Game > 10) {
                    switch (DebTxt) {
                        default:
                        case 0x00:
                            onoff[6] = "  PlayGo Finished";
                            break;
                        case 0x01:
                            onoff[6] = "  PGo Speed TRICKLE";
                            break;
                        case 0x02:
                            onoff[6] = "  (null) - This Goes Below PGo Speed TRICKLE";
                            break;
                    }
                } else {
                    switch (DebTxt) {
                        default:
                        case 0x00:
                            onoff[6] = "  Auto Save Slot &d";
                            break;
                        case 0x01:
                            onoff[6] = "  PLAYGO ACTIVE";
                            break;
                        case 0x02:
                            onoff[6] = "  Fade 1.00";
                            break;
                        case 0x03:
                            onoff[6] = "  Vol Samples 64";
                            break;
                    }
                }

                string[] lst = new string[] {
                    "  Add \"Build: \" Text Before Build Number In eboot.bin ",
                    "  Add \"Build: \" Text Before Build Number In param.sfo ",
                    "  Display Written Build Number In The cmd Window For A Half-Second Before Closing ",
                    "  Check For A Null Byte Before The Build Text In The .sfo, And Add A Space If There Is One (So The Text Actually Shows) ",
                    "  Write A Null Byte After The Build Text To Avoid Displaying The Leftovers Of Whatever You Replaced ",
                    "  Include Updated Build Date & Time (If UC1/2/3 Is Chosen, A Patch That Re-Enables This Will Also Be Applied) ",
                    "  Choose Which Debug Text To Replace With The Build Text: "
                };
                lst[SetInd] = "> " + lst[SetInd].TrimStart(new char[] {' '});
                W("Look Over These Settings - Move\\Make Changes With The Arrow Keys.\nPress Enter To Save Them And Continue.\n");

                CFS: // Prevent An Unlikely Exception From blankpace();
                int st = 0;
                foreach (string l in lst) {
                    if (l.Length > Console.WindowWidth) {
                        if (l.Length < Console.LargestWindowWidth) {
                            Console.WindowWidth = Console.BufferWidth = l.Length;
                             goto CFS;
                           }
                        else {
                            lst[st] = lst[st].Remove(Console.LargestWindowWidth - 5);
                            Console.WindowWidth = Console.LargestWindowWidth;
                            goto CFS;
                           }
                        }
                    st++;
                }

                for (int n = 0; n < lst.Length-1; n++) { // Cycle Through The lst[] array and display the text with it's corresponding Bool
                    Console.Write(lst[n]); CW(onoff[n] + blankspace(lst[n].Length + onoff[n].Length), bls[n]);
                }
                
                if (gameE == 0x42 || SetupTypeF == 0x02) // Skip Choosing The Debug Text If They're Using A Custom Eboot Addr Or Not Even Writing To The Eboot
                    goto ctsm;
                W($"\n{lst[lst.Length-1]}   "); CW(onoff[lst.Length-1] + blankspace(onoff[lst.Length-1].Length), true);
ctsm:

                var key = Console.ReadKey(true);
                switch (key.Key) {
                    default: goto top;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        break;
                    case ConsoleKey.Backspace:
                        goto top;
                    case ConsoleKey.UpArrow:
                        if (SetInd >0) {
                            SetInd--;
                        } else SetInd = (gameE == 0x42 | SetupTypeF == 0x02) ? 5 : 6;
                        goto top;
                    case ConsoleKey.DownArrow:
                        if (SetInd < (gameE == 0x42 | SetupTypeF == 0x02 ? 5 : 6))
                            SetInd++;
                        else SetInd = 0;
                        goto top;
                    case ConsoleKey.LeftArrow:
                        if (SetInd <6) {
                            bls[SetInd] = !bls[SetInd];
                            goto top;
                        }
                        if (DebTxt == 0) {
                            DebTxt = (byte)(Game <27 && Game > 10 ? 2 : 3); 
                        } else DebTxt--;
                        goto top;
                    case ConsoleKey.RightArrow:
                        if (SetInd <6) {
                            bls[SetInd] = !bls[SetInd];
                            goto top;
                        }
                        if (DebTxt == (byte)(Game <27 && Game > 10 ? 2 : 3)) {
                            DebTxt = 0;
                        } else DebTxt++;
                        goto top;
                }
            }

            int[] GetEbootAddr() {
                /*
                 * 0 AUTO-SAVE SLOT X (PlayGo Finished)
                 * 1 PLAYGO ACTIVE (PGo Speed TRICKLE)
                 * 2 Fade 1.00 (null)
                 * 3 Vol Samples 64
                 */
                switch (gameE) {
                    default:
                        CW($"tabarnack - exe addr error; 0x{gameE.ToString("X")}\n", false); R();
                        TE = 0xFFFFFFF;
                        return new int[] { 0xFFFFFFF };
                    case 0x00:
                        TE = 0x1024FD4;
                        return new int[] { 0x10DB8B4, 0x10DB8D8, 0x10DB8E7, 0x10DB8F2 }; // T1R 1.00 CUSA00552
                    case 0x01:
                        TE = 0x1091D44;
                        return new int[] { 0x1127F74, 0x1127F98, 0x1127FA7, 0x1127FB2 }; // T1R 1.09 CUSA00552
                    case 0x02:
                        TE = 0x1092124;
                        return new int[] { 0x1128524, 0x1128548, 0x1128557, 0x1128562 }; // T1R 1.1X CUSA00552
                    case 0x03:
                        TE = 0x29AE0F8;
                        return new int[] { 0x29682BF, 0x2968303, 0x2968348, 0x2968353 }; // T2 1.00 CUSA07820
                    case 0x04:
                        TE = 0x29D1CC8;
                        return new int[] { 0x298B7A1, 0x298B7E5, 0x298B86C, 0x298B877 }; // T2 1.01 CUSA07820
                    case 0x05:
                        TE = 0x29C18F8;
                        return new int[] { 0x297B3D1, 0x297B415, 0x297B49C, 0x297B4A7 }; // T2 1.02 CUSA07820
                    case 0x06:
                        TE = 0x29DB068;
                        return new int[] { 0x2994B13, 0x2994B57, 0x2994BDE, 0x2994BE9 }; // T2 1.05 CUSA07820
                    case 0x07:
                        TE = 0x29DC6E8;
                        return new int[] { 0x2996164, 0x29961A8, 0x299622F, 0x299623A }; // T2 1.07 CUSA07820
                    case 0x08:
                        TE = 0x2ACE208;
                        return new int[] { 0x2AA749E, 0x2AA74E2, 0x2AA7569, 0x2AA7574 }; // T2 1.08 CUSA07820
                    case 0x09:
                        TE = 0x2ACE248;
                        return new int[] { 0x2AA74D6, 0x2AA751A, 0x2AA75A1, 0x2AA75AC }; // T2 1.09 CUSA07820
                    case 0x10:
                        TE = 0x77B2E4;
                        return new int[] { 0x7DE99D, 0x7DE9C1 }; // UCC_UC1 1.00 CUSA02320
                    case 0x11:
                        TE = 0x772B84;
                        return new int[] { 0x7D616D, 0x7D6191 }; // UCC_UC1 1.02 CUSA02320
                    case 0x12:
                        return new int[] { 0xB485F2, 0xB48616 }; // UCC_UC2 1.00 CUSA02320
                    case 0x13:
                        return new int[] { 0xE7DDE7, 0xE7DE0B }; // UCC_UC2 1.02 CUSA02320
                    case 0x14:
                        return new int[] { 0xF682AB, 0xF682CF }; // UCC_UC3 1.00 CUSA02320
                    case 0x15:
                        return new int[] { 0x145F767, 0x145F78B }; // UCC_UC3 1.02 CUSA02320
                    case 0x16:
                        TE = 0x1E8D748;
                        return new int[] { 0x1FD4930, 0x1FD4968, 0x1FD4977, 0x1FD4982 }; // UC4 1.00 CUSA00341
                    case 0x17:
                        TE = 0x1EB2BDC;
                        return new int[] { 0x1E4B736, 0x1E4B772, 0x1E4B781, 0x1E4B78C }; // UC4 1.3X CUSA00341
                    case 0x18:
                        TE = 0x1FE670C;
                        return new int[] { 0x1F7CEC8, 0x1F7CEF0, 0x1F7CEFF, 0x1F7CF0A }; // TLL 1.00 CUSA07737
                    case 0x19:
                        TE = 0x1FF187C;
                        return new int[] { 0x1F877FB, 0x1F87823, 0x1F87832, 0x1F8783D }; // TLL 1.0X CUSA07737
                    case 0x42:
                  //    TE = GoFuckYourselfI'mTooLazyToSupportThisEvenThoIt'dTakeMinutesToDo 
                        return new int[] { CE, CE2, CE3 };
                }
            }

            int[] GetParamAddr() {
                switch (gameP) {
                    default:
                        CW($"tabarnack - sfo addr error; 0x{gameP.ToString("X")}\n", false);
                        return new int[] { 0xFFF };
                    case 0x00:
                        return new int[] { 0x5D8 }; // T1R 1.00 CUSA00552
                    case 0x01:
                        return new int[] { 0x674 }; // T1R 1.09/1.10 CUSA00552
                    case 0x02:
                        return new int[] { 0x698 }; // T1R 1.11 CUSA00552 
                    case 0x03:
                        return new int[] { 0x680, 0x703, 0x783 }; // T1R 1.11 CUSA00556
                    case 0x04:
                        return new int[] { 0x701, 0x780, 0x802, 0x882, 0x902, 0x98E, 0xA02, 0xA82 }; // T2 1.00 to 1.01 CUSA07820
                    case 0x05:
                        return new int[] { 0x725, 0x7A4, 0x826, 0x8A6, 0x926, 0x9B2, 0xA26, 0xAA6 }; // T2 1.02 to 1.09 CUSA07820
                    case 0x06:
                        return new int[] { 0x5DA }; // UCC 1.0X CUSA2320
                    case 0x07:
                        return new int[] { 0x611, 0x69E, 0x70E }; // UCC 1.0X CUSA2344
                    case 0x08:
                        return new int[] { 0x68F, 0x70F, 0x78F, 0x80F, 0x88F, 0x90F, 0x98F }; // UC4 1.00 CUSA00341
                    case 0x09:
                        return new int[] { 0x70F, 0x78F, 0x80F, 0x88F, 0x90F, 0x98F, 0xA0F }; // UC4 1.3X CUSA00341
                    case 0x10: // UC4 1.3X CUSA00918
                        return new int[] { 0x82B, 0x8AB, 0x92B, 0x9B4, 0xA2B, 0xAAC, 0xB2B, 0xBAF, 0xC2D, 0xCAB, 0xD2B, 0xDAB, 0xE2B, 0xEAB, 0xF2B, 0xFAB, 0x1030, 0x10AB, 0x1132, 0x11AB }; // UC4 1.3X CUSA00918
                    case 0x11:
                        return new int[] { 0x651 }; // TLL 1.00
                    case 0x12:
                        return new int[] { 0x675 }; // TLL 1.0X
                    case 0x42:
                        return new int[] { CP };
                }
            }
#endregion
            /*======================================================================================================================================================================================================================================
            ========================================================================================================================================================================================================================================
            [REGION - Main Functions || Create() Settings File If There Isn't One | Read Settings File And Write() To File\Files If There Is]
            ======================================================================================================================================================================================================================================
            =======================================================================================================================================================================================================================================*/

            if (DEL)
                File.Delete($@"{Directory.GetCurrentDirectory()}\Params.blb");
            if (File.Exists($@"{Directory.GetCurrentDirectory()}\Params.blb")) Write();
            else Create();

            void Create() {
                Console.CursorVisible = false;
                UCC = false;
                GameInd = Page = 0; Game = 69;


                Console.WriteLine("No Settings File Found, Going Through Initial Setup.", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                Thread.Sleep(115*7);
CMain:
                Console.CursorVisible = false;
                string[][] GamesList = new string[][] {
                    new string[] {
                "  Setup Type: Write To [eboot.bin & param.sfo] - [eboot.bin Only] - [param.sfo Only]",
                "  Use Custom Offsets For: [None, Use Built-In Offsets For Both] - [eboot.bin Only] - [param.sfo Only] - [eboot.bin & param.sfo] ",
                "",
                "  1. The Last of Us Remastered 1.00 CUSA00552",
                "  2. The Last of Us Remastered 1.09/1.10 CUSA00552",
                "  3. The Last of Us Remastered 1.11 CUSA00552",
                "  4. The Last of Us Remastered 1.11 CUSA00556",
                "  5. The Last of Us Part II 1.00 CUSA07820",
                "  6. The Last of Us Part II 1.01 CUSA07820",
                "  7. The Last of Us Part II 1.02 CUSA07820",
                "  8. The Last of Us Part II 1.05 CUSA07820",
                "  9. The Last of Us Part II 1.07 CUSA07820",
                "  10. The Last of Us Part II 1.08 CUSA07820",
                "  11. The Last of Us Part II 1.09 CUSA07820"
                    },
                    new string[] {
                "  Setup Type: Write To [eboot.bin & param.sfo] - [eboot.bin Only] - [param.sfo Only]",
                "  Use Custom Offsets For: [None, Use Built-In Offsets For Both] - [eboot.bin Only] - [param.sfo Only] - [eboot.bin & param.sfo] ",
                "",
                "  12. Uncharted: The Nathan Drake Collection | Uncharted 1 1.00 CUSA02320",
                "  13. Uncharted: The Nathan Drake Collection | Uncharted 1 1.02 CUSA02320",
                "  14. Uncharted: The Nathan Drake Collection | Uncharted 2 1.00 CUSA02320",
                "  15. Uncharted: The Nathan Drake Collection | Uncharted 2 1.02 CUSA02320",
                "  16. Uncharted: The Nathan Drake Collection | Uncharted 3 1.00 CUSA02320",
                "  17. Uncharted: The Nathan Drake Collection | Uncharted 3 1.02 CUSA02320",
                "  18. Uncharted: The Nathan Drake Collection | Uncharted 1 1.00 CUSA02344",
                "  19. Uncharted: The Nathan Drake Collection | Uncharted 1 1.02 CUSA02344",
                "  20. Uncharted: The Nathan Drake Collection | Uncharted 2 1.00 CUSA02344",
                "  21. Uncharted: The Nathan Drake Collection | Uncharted 2 1.02 CUSA02344",
                    },
                    new string[] {
                "  Setup Type: Write To [eboot.bin & param.sfo] - [eboot.bin Only] - [param.sfo Only]",
                "  Use Custom Offsets For: [None, Use Built-In Offsets For Both] - [eboot.bin Only] - [param.sfo Only] - [eboot.bin & param.sfo] ",
                "",
                "  22. Uncharted: The Nathan Drake Collection | Uncharted 3 1.00 CUSA02344",
                "  23. Uncharted: The Nathan Drake Collection | Uncharted 3 1.02 CUSA02344",
                "  24. Uncharted: The Nathan Drake Collection | All 3 Uncharted Games 1.00 CUSA02320",
                "  25. Uncharted: The Nathan Drake Collection | All 3 Uncharted Games 1.02 CUSA02320",
                "  26. Uncharted: The Nathan Drake Collection | All 3 Uncharted Games 1.00 CUSA02344",
                "  27. Uncharted: The Nathan Drake Collection | All 3 Uncharted Games 1.02 CUSA02344",
                "  28. Uncharted 4: A Thief's End 1.00 CUSA00341",
                "  29. Uncharted 4: A Thief's End 1.32/1.33 CUSA00341",
                "  30. Uncharted 4: A Thief's End 1.32/1.33 CUSA00918",
                "  31. Uncharted: The Lost Legacy 1.00 CUSA07737",
                "  32. Uncharted: The Lost Legacy 1.08/1.09 CUSA07737"
                    }
                };

                
                CF: // Prevent An Exception From blankpace();
                for (int t = 0; t <3; t++) { int st = 0;
                    foreach (string l in GamesList[t]) {
                        if (l.Length > Console.WindowWidth) {
                            if (l.Length < Console.LargestWindowWidth) {
                                Console.WindowWidth = l.Length;
                                goto CMain;
                            } else {
                                GamesList[t][st] = GamesList[t][st].Remove(Console.LargestWindowWidth - 1);
                                Console.WindowWidth = Console.LargestWindowWidth;
                                goto CF;
                            }
                        }
                        st++;
                    }
                }

                Console.SetCursorPosition(0, 0);
                W("Choose Your Game And Setup Type, Or Go With Custom Offsets For Both eboot.bin & param.sfo");
                CW($"                                                                                                           [S To View Settings]{blankspace(128)}\n", true);

                Console.ResetColor();

                W($"Note: The Title I.D. (CUSAXXXXX) Only Matters For the param.sfo offsets, Not The eboot.bin.{blankspace(92)}\n{blankspace(1)}");


                GamesList[Page][GameInd] = "> " + GamesList[Page][GameInd].TrimStart(new char[] { ' ' });
                for (int i = 0; i < GamesList[Page].Length; i++) {
                    Console.Write($"{GamesList[Page][i]}{blankspace(GamesList[Page][i].Length)}");
                }

                Console.Write($"  Page {PageNum()}...{blankspace(PageNum().Length)}");
                for (int i = 0; i < 7; i++) { // Clear The Other Lines
                    W("                                                                                        ");
                }
                Console.SetCursorPosition(0, Page == 1 ? GameInd + 1: GameInd);
                switch (SetupTypeF) {
                    case 0x00:
                        Console.SetCursorPosition(23, 4);
                        CW("[eboot.bin & param.sfo]", true);
                        break;
                    case 0x01:
                        Console.SetCursorPosition(49, 4);
                        CW("[eboot.bin Only]", true);
                        break;
                    case 0x02:
                        Console.SetCursorPosition(68, 4);
                        CW("[param.sfo Only]", true);
                        break;
                }
                switch (SetupTypeC) {
                    case 0x00:
                        Console.SetCursorPosition(26, 5);
                        CW("[None, Use Built-In Offsets For Both]", true);
                        break;
                    case 0x01:
                        Console.SetCursorPosition(66, 5);
                        CW("[eboot.bin Only]", true);
                        break;
                    case 0x02:
                        Console.SetCursorPosition(85, 5);
                        CW("[param.sfo Only]", true);
                        break;
                    case 0x03:
                        Console.SetCursorPosition(104, 5);
                        CW("[eboot.bin & param.sfo]", true);
                        break;

                }
                switch (R()) {
                    default:
                        goto CMain;
                    case ConsoleKey.S:
                        Viewettings();
                        goto CMain;
                    case ConsoleKey.Enter:
                        if (GameInd == 0 | GameInd == 1)
                            goto CMain;

                        if (Page == 0) {
                            Game = GameInd - 3;
                        }
                        else if (Page == 1) {
                            Game = GameInd + 8;
                        }
                        else if (Page == 2) {
                            Game = GameInd + 18;
                            if (GameInd > 4 & GameInd < 9) {
                                UCC = true;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (GameInd == 0 & Page != 0) {
                            IsLastPage = false;
                            Page--; GameInd = GamesList[Page].Length - 1;
                        } else if (GameInd == 0 & Page == 0) {
                            Page = 2; GameInd = GamesList[Page].Length - 1; IsLastPage = true;
                        } else if (GameInd != 0) {
                            if (GameInd == 3) {
                                GameInd = 1;
                            } else GameInd--;
                        }
                        goto CMain;
                    case ConsoleKey.DownArrow:
                        if (GameInd == GamesList[Page].Length - 1 & Page == 2) {
                            Page = 0; GameInd = 0; IsLastPage = false;
                        } else if (GameInd != GamesList[Page].Length - 1) {
                             if (GameInd == 1) {
                                GameInd = 3;
                            } else GameInd++;
                        } else {
                            Page++;
                            GameInd = 0;
                            IsLastPage = Page == 2 ? true : false;
                        }
                        goto CMain;
                    case ConsoleKey.LeftArrow:
                        if (GameInd == 0) {
                            if (SetupTypeF == 0x00) {
                                SetupTypeF = 0x02;
                            } else SetupTypeF--;
                            goto CMain;
                        } else if (GameInd == 1) {
                            if (SetupTypeC == 0x00) {
                                SetupTypeC = 0x03;
                            } else SetupTypeC--;
                            goto CMain;
                        }
                        goto CMain;
                    case ConsoleKey.RightArrow:
                        if (GameInd == 0) {
                            if (SetupTypeF == 0x02) {
                                SetupTypeF = 0x00;
                            } else SetupTypeF++;
                            goto CMain;
                        }
                        else if (GameInd == 1) {
                            if (SetupTypeC == 0x03) {
                                SetupTypeC = 0x00;
                            } else SetupTypeC++;
                            goto CMain;
                        }
                        goto CMain;
                }

                switch (Game) {
                    default:
                        gameE = gameP = 0xFF;
                        CW("Key Case ERR\n", false);
                        R();
                        break;
                    case 0: // T1R 1.00 CUSA00552
                        gameE = 0x00;
                        gameP = 0x00;
                        break;
                    case 1: // T1R 1.09 CUSA00552
                        gameE = 0x01;
                        gameP = 0x01;
                        break;
                    case 2: // T1R 1.1X CUSA00552
                        gameE = 0x02;
                        gameP = 0x02;
                        break;
                    case 3: // T1R 1.1X CUSA00556
                        gameE = 0x02;
                        gameP = 0x03;
                        break;
                    case 4: // T2 1.00 CUSA07820
                        gameE = 0x03;
                        gameP = 0x04;
                        break;
                    case 5: // T2 1.01 CUSA07820
                        gameE = 0x04;
                        gameP = 0x04;
                        break;
                    case 6: // T2 1.02 CUSA07820
                        gameE = 0x05;
                        gameP = 0x05;
                        break;
                    case 7: // T2 1.05 CUSA07820
                        gameE = 0x06;
                        gameP = 0x05;
                        break;
                    case 8: // T2 1.07 CUSA07820
                        gameE = 0x07;
                        gameP = 0x05;
                        break;
                    case 9: // T2 1.08 CUSA07820
                        gameE = 0x08;
                        gameP = 0x05;
                        break;
                    case 10: // T2 1.09 CUSA07820
                        gameE = 0x09;
                        gameP = 0x05;
                        break;
                    case 11: // UC1 1.00 CUSA02320
                        gameE = 0x10;
                        gameP = 0x06;
                        break;
                    case 12: // UC1 1.02 CUSA02320
                        gameE = 0x11;
                        gameP = 0x06;
                        break;
                    case 13: // UC2 1.00 CUSA02320
                        gameE = 0x12;
                        gameP = 0x06;
                        break;
                    case 14: // UC2 1.02 CUSA02320
                        gameE = 0x13;
                        gameP = 0x06;
                        break;
                    case 15: // UC3 1.00 CUSA02320
                        gameE = 0x14;
                        gameP = 0x06;
                        break;
                    case 16: // UC3 1.02 CUSA02320
                        gameE = 0x15;
                        gameP = 0x06;
                        break;
                    case 17: // UC1 1.00 CUSA02344
                        gameE = 0x10;
                        gameP = 0x07;
                        break;
                    case 18: // UC1 1.02 CUSA02344
                        gameE = 0x11;
                        gameP = 0x07;
                        break;
                    case 19: // UC2 1.00 CUSA02344
                        gameE = 0x12;
                        gameP = 0x07;
                        break;
                    case 20: // UC2 1.02 CUSA02344
                        gameE = 0x13;
                        gameP = 0x07;
                        break;
                    case 21: // UC3 1.00 CUSA02344
                        gameE = 0x14;
                        gameP = 0x07;
                        break;
                    case 22: // UC3 1.02 CUSA02344
                        gameE = 0x15;
                        gameP = 0x07;
                        break;
                    case 23: // All Three UCC Games 1.00 CUSA02320
                        gameE = 0xF5;
                        gameP = 0x06;
                        break;
                    case 24: // All Three UCC Games 1.02 CUSA02320
                        gameE = 0xF6;
                        gameP = 0x06;
                        break;
                    case 25: // All Three UCC Games 1.00 CUSA02344
                        gameE = 0xF5;
                        gameP = 0x07;
                        break;
                    case 26: // All Three UCC Games 1.02 CUSA02344
                        gameE = 0xF6;
                        gameP = 0x07;
                        break;
                    case 27: // UC4 1.00 CUSA00341
                        gameE = 0x16;
                        gameP = 0x08;
                        break;
                    case 28: // UC4 1.3X CUSA00341
                        gameE = 0x17;
                        gameP = 0x09;
                        break;
                    case 29: // UC4 1.3X CUSA00918
                        gameE = 0x17;
                        gameP = 0x10;
                        break;
                    case 30: // TLL 1.00 CUSA07737
                        gameE = 0x18;
                        gameP = 0x11;
                        break;
                    case 31: // TLL 1.0X CUSA07737
                        gameE = 0x19;
                        gameP = 0x12;
                        break;
                    case 42: // Custom Offsets
                        gameE = 0x42;
                        gameP = 0x42;
                        break;
                    case 255: // Go Back
                        goto CMain;
                }
C2:
                Console.Clear();
                Console.CursorVisible = true;
                W("Go Back With Escape Or Backspace");
                try {
                    switch (SetupTypeC) {

                        case 0x00: // Both Default Addresses
                            W("Using Built-In Addresses For eboot.bin & param.sfo...");
                            break;

                        case 0x01: // Eboot Only

                            DebTxt = 0x00; gameE = 0x42;
                            W("Setup Type: Custom eboot.bin Address With The Built-In param.sfo Address...\n");

                            C3:
                            W("\nEnter The Address Of Your Custom Build Text For The eboot.bin");
                            string st1 = ReadLine(Console.CursorTop);
                            if (st1 == "\n") goto CMain;
                            if (st1.Length > 1 && st1.ToCharArray()[1] == 'x') {
                                CE = Convert.ToInt32(st1, 16);
                                D($"\n|{st1} | {CE}");
                            } else {
                                CE = int.Parse(st1, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st1} | {CE}");
                            }

                            if (!UCC) break; C4:
                            
                            W("\nEnter The Address Of Your Custom Build Text For The Uncharted 2 Executable");
                            st1 = ReadLine(Console.CursorTop);
                            if (st1 == "\n") goto C3;
                            if (st1.Length > 1 && st1.ToCharArray()[1] == 'x') {
                                CE2 = Convert.ToInt32(st1, 16);
                                D($"\n|{st1} | {CE2}");
                            } else {
                                CE2 = int.Parse(st1, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st1} | {CE2}");
                            }

                            W("\nEnter The Address Of Your Custom Build Text For The Uncharted 3 Executable");
                            st1 = ReadLine(Console.CursorTop);
                            if (st1 == "\n") goto C4;
                            if (st1.Length > 1 && st1.ToCharArray()[1] == 'x') {
                                CE3 = Convert.ToInt32(st1, 16);
                                D($"\n|{st1} | {CE3}");
                            } else {
                                CE3 = int.Parse(st1, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st1} | {CE3}");
                            }
                            break;

                        case 0x02: // Param.sfo Only
                            gameP = 0x42;
                            W("Setup Type: Custom param.sfo offset And The Built-In Eboot Address\n\nEnter The Address Of Your Custom Build Text For The param.sfo");
                            string st2 = ReadLine(4);
                            if (st2 == "\n") goto CMain;
                            if (st2.Length > 1 && st2.ToCharArray()[1] == 'x') {
                                CP = Convert.ToInt32(st2, 16);
                                D($"\n|{st2} | {CP}");
                            } else {
                                CP = int.Parse(st2, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st2} | {CP}");
                            }
                            break;

                        case 0x03: // Both Custom Addresses
                            C6:
                            DebTxt = 0x00;
                            gameE = gameP = 0x42;
                            W("Setup Type: Custom Address For Both The eboot.bin & param.sfo\n");

                            if (SetupTypeF == 0x02) goto OneGame;
                            W("\nEnter The Address Of Your Custom Build Text For The eboot.bin");
                            string st3 = ReadLine(Console.CursorTop);
                            if (st3 == "\n") goto CMain;
                            if (st3.Length > 1 && st3.ToCharArray()[1] == 'x') {
                                CE = Convert.ToInt32(st3, 16);
                                D($"\n|{st3} | {CE}");
                            } else {
                                CE = int.Parse(st3, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st3} | {CE}");
                            }

                            if (!UCC) goto OneGame; C7:

                            W("\nEnter The Address Of Your Custom Build Text For The Uncharted 2 Executable");
                            st3 = ReadLine(Console.CursorTop);
                            if (st3 == "\n") goto C6;
                            if (st3.Length > 1 && st3.ToCharArray()[1] == 'x') {
                                CE2 = Convert.ToInt32(st3, 16);
                                D($"\n|{st3} | {CE2}");
                            } else {
                                CE2 = int.Parse(st3, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st3} | {CE2}");
                            } C8:
                            W("\nEnter The Address Of Your Custom Build Text For The Uncharted 3 Executable");
                            st3 = ReadLine(Console.CursorTop);
                            if (st3 == "\n") goto C7;
                            if (st3.Length > 1 && st3.ToCharArray()[1] == 'x') {
                                CE3 = Convert.ToInt32(st3, 16);
                                D($"\n|{st3} | {CE3}");
                            } else {
                                CE3 = int.Parse(st3, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st3} | {CE3}");
                            }
OneGame:
                            if (SetupTypeF == 0x01) break;
                            W("\nEnter The Address Of Your Custom Build Text For The param.sfo");
                            st3 = ReadLine(Console.CursorTop);
                            if (st3 == "\n") goto C8;
                            if (st3.Length > 1 && st3.ToCharArray()[1] == 'x') {
                                CP = Convert.ToInt32(st3, 16);
                                D($"\n|{st3} | {CP}");
                            } else {
                                CP = int.Parse(st3, System.Globalization.NumberStyles.HexNumber);
                                D($"\n|{st3} | {CP}");
                            }
                            break;
                    }
                } catch (Exception tabarnack) {
                    Console.WriteLine(tabarnack);
                    R(); Console.Clear();
                    goto CMain;
                }

Path:
                W("\nEnter Folder Path: ");
                path = ReadLine(Console.CursorTop);
                if (path == "\n") {
                    if (SetupTypeC != 0x00) goto C2;
                    else goto CMain;
                }
                else if (path.Length == 0) {
                    CW("You Didn't Even Type Anything, Dumbass...\n", false);
                    goto Path;
                }
                len = path.Length;
                D($"\n|Path Length: {len}\n|Path: {path}");

Int:           
                W("\nEnter Build Number - 999 Or Lower:\n");
                try {
                    tmpS = ReadLine(Console.CursorTop);
                    if (tmpS == "\n") goto Path;
                    else if (tmpS.Length == 0) {
                        CW("You Didn't Even Type Anything, Dumbass...\n", false);
                        goto Int;
                    }
                    buildi = Convert.ToInt32(tmpS.Contains(".") ? tmpS.Remove(tmpS.IndexOf('.'), 1) : tmpS) - 1;
                    D($"\n|{tmpS}\n|{buildi}");
                }
                catch (FormatException) {
                    D($"\n|{tmpS}");
                    CW("\nString Contains Invalid Characters\n", false);
                    goto Int;
                }
                Viewettings();

                CW("Depending On The Chosen Game, The Replaced Debug Text May Not Be Enabled By Default. I'll Add An Option To Patch Them On By Default Later\n", false);
                W("(Press Any Key To Finish And Write)");

                using (FileStream make = new FileStream(Directory.GetCurrentDirectory() + @"\Params.blb", FileMode.Create, FileAccess.ReadWrite)) {
                    make.WriteByte(gameE);
                    make.WriteByte(gameP);
                    make.WriteByte(SetupTypeC);
                    make.WriteByte(SetupTypeF);
                    if (UCC) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[0]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[1]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[2]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[3]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[4]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    if (bls[5]) {
                        make.WriteByte(0x01);
                    } else make.WriteByte(0x00);
                    make.WriteByte(DebTxt);

                    switch (SetupTypeC) {
                        case 0x00:
                            make.Write(BitConverter.GetBytes(buildi), 0, 4);
                            make.Write(BitConverter.GetBytes(len), 0, 4);
                            make.Write(Encoding.ASCII.GetBytes(path), 0, len);
                            break;
                        case 0x01:
                            make.Write(BitConverter.GetBytes(CE), 0, 4);
                            if (UCC) {
                                make.Write(BitConverter.GetBytes(CE2), 0, 4);
                                make.Write(BitConverter.GetBytes(CE3), 0, 4);
                            }
                            make.Write(BitConverter.GetBytes(buildi), 0, 4);
                            make.Write(BitConverter.GetBytes(len), 0, 4);
                            make.Write(Encoding.ASCII.GetBytes(path), 0, len);
                            break;
                        case 0x02:
                            make.Write(BitConverter.GetBytes(CP), 0, 4);
                            make.Write(BitConverter.GetBytes(buildi), 0, 4);
                            make.Write(BitConverter.GetBytes(len), 0, 4);
                            make.Write(Encoding.ASCII.GetBytes(path), 0, len);
                            break;
                        case 0x03:
                            make.Write(BitConverter.GetBytes(CE), 0, 4);
                            if (UCC) {
                                make.Write(BitConverter.GetBytes(CE2), 0, 4);
                                make.Write(BitConverter.GetBytes(CE3), 0, 4);
                            }
                            make.Write(BitConverter.GetBytes(CP), 0, 4);
                            make.Write(BitConverter.GetBytes(buildi), 0, 4);
                            make.Write(BitConverter.GetBytes(len), 0, 4);
                            make.Write(Encoding.ASCII.GetBytes(path), 0, len);
                            break;
                    }
                } // Read The Params.blb Once Finished Creating it
                Write();
            }

/*======================================================================================================================================================================================================================================
========================================================================================================================================================================================================================================

[REGION - Write() =|= Reads Params.blb File And Writes To The executable(s) & / Or .sfo

======================================================================================================================================================================================================================================
=======================================================================================================================================================================================================================================*/

            void Write() {
                try {
                    using (FileStream RS = new FileStream(Directory.GetCurrentDirectory() + @"\Params.blb", FileMode.Open, FileAccess.ReadWrite)) {
                        gameE = (byte)RS.ReadByte();
                        gameP = (byte)RS.ReadByte();
                        SetupTypeC = (byte)RS.ReadByte();
                        SetupTypeF = (byte)RS.ReadByte();
                        if ((byte)RS.ReadByte() == 0x01) UCC = true;
                        else UCC = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[0] = true;
                        else bls[0] = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[1] = true;
                        else bls[1] = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[2] = true;
                        else bls[2] = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[3] = true;
                        else bls[3] = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[4] = true;
                        else bls[4] = false;
                        if ((byte)RS.ReadByte() == 0x01) bls[5] = true;
                        else bls[5] = false;
                        DebTxt = (byte)RS.ReadByte();


                        string add = "Offset Used";
                        data = new byte[4];
                        switch (SetupTypeC) {
                            default:
                                CE = CP = 0;
                                buildi = 0;
                                len = 7;
                                path = "[Error]";
                                break;
                            case 0x00:
                                W("No Custom Offsets Used...");
                                RS.Read(data, 0, 4);
                                buildi = BitConverter.ToInt32(data, 0)+1;
                                RS.Position = RS.Position - 4;
                                RS.Write(BitConverter.GetBytes(buildi), 0, 4);

                                RS.Read(data, 0, 4);
                                len = BitConverter.ToInt32(data, 0);

                                data = new byte[len];
                                RS.Read(data, 0, len);
                                path = Encoding.ASCII.GetString(data);
                                CE = CP = 0;
                                break;
                            case 0x01:
                                RS.Read(data, 0, 4);
                                CE = BitConverter.ToInt32(data, 0);
                                if (UCC) {
                                    add = "Offsets Used For All Three UCC Games";
                                    RS.Read(data, 0, 4);
                                    CE2 = BitConverter.ToInt32(data, 0);
                                    RS.Read(data, 0, 4);
                                    CE3 = BitConverter.ToInt32(data, 0);
                                }
                                W($"Custom eboot.bin {add}...");
                                RS.Read(data, 0, 4);
                                buildi = BitConverter.ToInt32(data, 0)+1;
                                RS.Position = RS.Position - 4;
                                RS.Write(BitConverter.GetBytes(buildi), 0, 4);

                                RS.Read(data, 0, 4);
                                len = BitConverter.ToInt32(data, 0);

                                data = new byte[len];
                                RS.Read(data, 0, len);
                                path = Encoding.ASCII.GetString(data);
                                break;
                            case 0x02:
                                W("Custom Param .sfo Offsets Used...");
                                RS.Read(data, 0, 4);
                                CP = BitConverter.ToInt32(data, 0);

                                RS.Read(data, 0, 4);
                                buildi = BitConverter.ToInt32(data, 0)+1;
                                RS.Position = RS.Position - 4;
                                RS.Write(BitConverter.GetBytes(buildi), 0, 4);

                                RS.Read(data, 0, 4);
                                len = BitConverter.ToInt32(data, 0);

                                data = new byte[len];
                                RS.Read(data, 0, len);
                                path = Encoding.ASCII.GetString(data);
                                break;
                            case 0x03:
                                RS.Read(data, 0, 4);
                                CE = BitConverter.ToInt32(data, 0);
                                if (UCC) {
                                    add = "Offsets Used For All Three UCC Games";
                                    RS.Read(data, 0, 4);
                                    CE2 = BitConverter.ToInt32(data, 0);
                                    RS.Read(data, 0, 4);
                                    CE3 = BitConverter.ToInt32(data, 0);
                                }
                                W($"Custom eboot.bin & param.sfo {add}...");
                                RS.Read(data, 0, 4);
                                CP = BitConverter.ToInt32(data, 0);

                                RS.Read(data, 0, 4);
                                RS.Position = RS.Position - 4;
                                buildi = BitConverter.ToInt32(data, 0)+1;
                                RS.Write(BitConverter.GetBytes(buildi), 0, 4);

                                RS.Read(data, 0, 4);
                                len = BitConverter.ToInt32(data, 0);

                                data = new byte[len];
                                RS.Read(data, 0, len);
                                path = Encoding.ASCII.GetString(data);
                                break;
                        }

                        flt = ((float)(buildi / 100.00)).ToString();
                        D($"|Path Length: {path.Length}");
                        if (!File.Exists(path + @"\eboot.bin")) {
                            W($"The File Specified (\"{path}\\eboot.bin\") Doesn't Exist.");
                            D($"|Path Length: {len}\n|Data Read: {BitConverter.ToString(data)}");
                            R();
                        }
                    }
                    if (SetupTypeF != 0x02 && !UCC) {
                        using (FileStream WS = new FileStream(path + @"\eboot.bin", FileMode.Open, FileAccess.ReadWrite)) {
                            WS.Position = GetEbootAddr()[DebTxt];
                            if (bls[0]) {
                                data = DebTxt == 0x02 // Change "Build: " To "Build " If The Debug Text Selected Is "Fade 1.00" To Avoid Overlapping Vol Samples 64
                                ? new byte[] { 0x42, 0x75, 0x69, 0x6C, 0x64, 0x20, 0x00, 0x00, 0x00, 0x00 }
                                : new byte[] { 0x42, 0x75, 0x69, 0x6C, 0x64, 0x3A, 0x20, 0x00, 0x00, 0x00, 0x00 };
                            }
                            else {
                                data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                            }
                            
                            Buffer.BlockCopy(Encoding.UTF8.GetBytes(flt), 0, data, data.Length - 4, flt.Length);
                            WS.Write(data, 0, data.Length);
                            
                            D($"|{BitConverter.ToString(data)} Written To Exe At 0x{GetEbootAddr()[DebTxt].ToString("X")}");


                            if (Encoding.UTF8.GetBytes(flt).Length < 4) {
                                D("|The Float Was Smaller Than Usual, Wrote 0x30");
                                WS.Position--;
                                WS.WriteByte(0x30);
                            }
                            if (bls[4]) WS.WriteByte(0x00);                            
                            
                            if (bls[5] && !NoDateText(gameE)) {
                                WS.Position = TE;
                                tmp = Time(gameE);
                                WS.Write(tmp, 0, tmp.Length);
                                if (bls[4]) WS.WriteByte(0x00);
                                D($"|Wrote Date & Time: {Encoding.ASCII.GetString(tmp)}");
                            }
                        }
                    }
                    
                    else if (UCC && SetupTypeF != 0x02) {
                        byte[] UCG; tmpI = 0;
                        switch (gameE) {
                            default:
                                UCG = new byte[] { gameE, gameE, gameE };
                                break;
                            case 0xF5:
                                UCG = new byte[] { 0x10, 0x12, 0x14 };
                                break;
                            case 0xF6:
                                UCG = new byte[] { 0x11, 0x13, 0x15 };
                                break;
                        }
                        if (bls[0]) {
                            data = DebTxt == 0x02 // Change "Build: " To "Build " If The Debug Text Selected Is "Fade 1.00" To Avoid Overlapping Vol Samples 64
                                ? new byte[] { 0x42, 0x75, 0x69, 0x6C, 0x64, 0x20, 0x00, 0x00, 0x00, 0x00 }
                                : new byte[] { 0x42, 0x75, 0x69, 0x6C, 0x64, 0x3A, 0x20, 0x00, 0x00, 0x00, 0x00 };
                            Buffer.BlockCopy(Encoding.UTF8.GetBytes(flt), 0, data, 7, flt.Length);
                        }
                        else {
                            data = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
                            Buffer.BlockCopy(Encoding.UTF8.GetBytes(flt), 0, data, 0, flt.Length);
                        }
                        if (bls[5] && tmpI == 0) {
                            tmp = Time(gameE);
                            D($"|Date & Time: {Encoding.ASCII.GetString(tmp)}");
                        }
                        foreach (string pth in paths) {
                            using (FileStream WS = new FileStream(path + pth, FileMode.Open, FileAccess.ReadWrite)) {
                                gameE = UCG[tmpI];

                                WS.Position = gameE == 0x42 ? GetEbootAddr()[0] : GetEbootAddr()[DebTxt];
                                D($"|DebTxt: 0x{DebTxt.ToString("X")}\n|0x{WS.Position.ToString("X")}");

                                WS.Write(data, 0, data.Length);
                                D($"|{BitConverter.ToString(data)} Written {path}{pth} At 0x{GetEbootAddr()[DebTxt].ToString("X")}");

                                if (Encoding.UTF8.GetBytes(flt).Length < 4) {
                                    D("|The FLoat Was Smaller Than Usual, Wrote 0x30");
                                    WS.Position--;
                                    WS.WriteByte(0x30);
                                }
                                if (bls[4]) WS.WriteByte(0x00);                                
                                
                                if (bls[5] && tmpI == 0) {
                                    WS.Position = TE;
                                    WS.Write(tmp, 0, tmp.Length);
                                    if (bls[4]) WS.WriteByte(0x00);       
                                }
                                tmpI++;
                            }
                        }
                    }

                    if (SetupTypeF != 0x01) {
                        using (FileStream WS_ = new FileStream(path + @"\sce_sys\param.sfo", FileMode.Open, FileAccess.ReadWrite)) {
                            foreach (int offset in GetParamAddr()) {
                                WS_.Position = gameP == 0x42 ? offset -1 : offset;
                                if (bls[3] & (byte)WS_.ReadByte() == 0x00) {
                                    WS_.Position = WS_.Position - 1;
                                    WS_.WriteByte(0x20);
                                    D("|.sfo Build Number Would Have Been Blocked Off By A Null Byte, Added A Space");
                                }
                                if (bls[1]) {
                                    data = new byte[] { 0x42, 0x75, 0x69, 0x6C, 0x64, 0x3A, 0x20, 0x00, 0x00, 0x00, 0x00 };
                                    Buffer.BlockCopy(Encoding.UTF8.GetBytes(flt), 0, data, 7, flt.Length);
                                } else {
                                    data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                                    Buffer.BlockCopy(Encoding.UTF8.GetBytes(flt), 0, data, 0, flt.Length);
                                }
                                WS_.Write(data, 0, data.Length);
                                D($"|{BitConverter.ToString(data)} Written To .sfo At 0x{WS_.Position.ToString("X")}");

                                if (Encoding.UTF8.GetBytes(flt).Length < 4) {
                                    D("|The FLoat Was Smaller Than Usual, Wrote 0x30");
                                    WS_.Position--;
                                    WS_.WriteByte(0x30);
                                }
                                if (bls[4]) WS_.WriteByte(0x00);
                            }
                        }
                    }
                    if (bls[2]) {
                        CW($"Finished! {flt} Written", true);
                        Thread.Sleep(115*7);
                    }
                    if (!REL)
                        Console.ReadKey();
                }
                catch (Exception tabarnackeh) {
                    Console.Write(tabarnackeh); R();
                }
            }
        }
    }
}