﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spark.Parser;
using Spark.Compiler;
using Spark.Parser.Syntax;
using Spark.FileSystem;

namespace SparkSense
{
    public class SparkViewAnalyzer
    {
        private ViewLoader _viewLoader;
        private string _viewPath;

        public SparkViewAnalyzer(IViewFolder viewRoot, string viewPath)
        {
            _viewLoader = new ViewLoader { ViewFolder = viewRoot, SyntaxProvider = new DefaultSyntaxProvider(new ParserSettings()) };
            _viewPath = viewPath;
        }

        public IList<string> GetLocalVariables()
        {
            var localVariables = new List<string>();
            var chunks = _viewLoader.Load(_viewPath);
            var locals = chunks.Where(chunk => chunk is LocalVariableChunk);
            locals.ToList().ForEach(local => localVariables.Add(((LocalVariableChunk)local).Name));
            return localVariables;
        }
    }
}
