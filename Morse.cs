using System;
using System.Collections.Generic;

namespace aviationLib
{
    public class Morse
    {
        public struct LetterCode
        {
            public String letter;
            public String code;
        }

        public List<LetterCode> list;

        public Morse()
        {
            list = new List<LetterCode>();

            list.Add(new LetterCode() { letter = "A", code = ".-" });
            list.Add(new LetterCode() { letter = "B", code = "-..." });
            list.Add(new LetterCode() { letter = "C", code = "-.-." });
            list.Add(new LetterCode() { letter = "D", code = "-.." });
            list.Add(new LetterCode() { letter = "E", code = "." });
            list.Add(new LetterCode() { letter = "F", code = "..-." });
            list.Add(new LetterCode() { letter = "G", code = "--." });
            list.Add(new LetterCode() { letter = "H", code = "...." });
            list.Add(new LetterCode() { letter = "I", code = ".." });
            list.Add(new LetterCode() { letter = "J", code = ".---" });
            list.Add(new LetterCode() { letter = "K", code = "-.-" });
            list.Add(new LetterCode() { letter = "L", code = ".-.." });
            list.Add(new LetterCode() { letter = "M", code = "--" });
            list.Add(new LetterCode() { letter = "N", code = "-." });
            list.Add(new LetterCode() { letter = "O", code = "---" });
            list.Add(new LetterCode() { letter = "P", code = ".--." });
            list.Add(new LetterCode() { letter = "Q", code = "--.-" });
            list.Add(new LetterCode() { letter = "R", code = ".-." });
            list.Add(new LetterCode() { letter = "S", code = "..." });
            list.Add(new LetterCode() { letter = "T", code = "-" });
            list.Add(new LetterCode() { letter = "U", code = "..-" });
            list.Add(new LetterCode() { letter = "V", code = "...-" });
            list.Add(new LetterCode() { letter = "W", code = ".--" });
            list.Add(new LetterCode() { letter = "X", code = "-..-" });
            list.Add(new LetterCode() { letter = "Y", code = "-.--" });
            list.Add(new LetterCode() { letter = "Z", code = "--.." });

            list.Add(new LetterCode() { letter = "0", code = "-----" });
            list.Add(new LetterCode() { letter = "1", code = ".----" });
            list.Add(new LetterCode() { letter = "2", code = "..---" });
            list.Add(new LetterCode() { letter = "3", code = "...--" });
            list.Add(new LetterCode() { letter = "4", code = "....-" });
            list.Add(new LetterCode() { letter = "5", code = "....." });
            list.Add(new LetterCode() { letter = "6", code = "-...." });
            list.Add(new LetterCode() { letter = "7", code = "--..." });
            list.Add(new LetterCode() { letter = "8", code = "---.." });
            list.Add(new LetterCode() { letter = "9", code = "----." });
        }
    }
}
