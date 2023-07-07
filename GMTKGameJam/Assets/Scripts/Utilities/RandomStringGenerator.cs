using UnityEngine;
using static System.String;
using Random = UnityEngine.Random;

namespace DragoRyu.Utilities
{
    public class RandomStringGenerator
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyz";
        private const string Numerics = "0123456789";

        private bool _includeNumerics;
        private bool _includeCapitalLetters;

        private int? _length;
        private NumberRange? _range = null;


        public RandomStringGenerator IncludeNumerics()
        {
            _includeNumerics = true;
            return this;
        }
        public RandomStringGenerator IncludeCapitalLetters()
        {
            _includeCapitalLetters = true;
            return this;
        }
        public RandomStringGenerator SetFixedLength(int length)
        {
            _length = length;
            return this;
        }
        public string Generate()
        {
            if(_length == null && _range==null)
            {
                Debug.LogError("Invalid Generation of Strings as GenerateRandomString() requires a Fixed Length or a Number Range. Try Calling GenerateRandomString(length:int)\nReturning Empty String");
                return Empty;
            }
            
            if(_length!=null)
                return GenerateRandomStringFixedLength();
            if(_range!=null)
                return GenerateRandomStringRange();

            return Empty;
        }
        private string GenerateRandomStringFixedLength()
        {
            return _length != null ? GenerateRandomString(_length.Value) : Empty;
        }
        private string GenerateRandomStringRange()
        {
            if (_range == null) return Empty;
            
            var length = Random.Range((int)_range.Value.Min, (int)_range.Value.Max+1);
            return GenerateRandomString(length);
        }
        public string GenerateRandomString(int length)
        {
            var output = Empty;
            for (var i = 0; i < length; i++)
            {
                var tmp =GetRandomStringSet();
                tmp = ""+GetRandomChar(tmp);
                tmp = GetRandomUpper(tmp);
                output += tmp;
            }
            return output;
        }
        private string GetRandomStringSet()
        {
            return _includeNumerics ? Random.Range(0, 2) == 1 ? Numerics : Characters: Characters;
        }
        public char GetRandomChar(string stringSet)
        {
            return stringSet[Random.Range(0, stringSet.Length)];
        }
        public string GetRandomUpper(string ch)
        {
            return Random.Range(0,2)==1?ch.ToUpper():ch;
        }
    }
}
