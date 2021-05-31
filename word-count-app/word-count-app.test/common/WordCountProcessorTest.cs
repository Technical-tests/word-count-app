using AutoBogus;

using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using word_count_app.common.Helpers;
using word_count_app.common.Models;

using Xunit;

namespace word_count_app.test.common
{
    public class WordCountProcessorTest
    {        
        private RequestWords _requestWords;
        private readonly WordCountProcessor _wordCountProcessor;

        public WordCountProcessorTest()
        {
            _requestWords = AutoFaker.Generate<RequestWords>();            
            _wordCountProcessor = new WordCountProcessor();
        }

        [Fact]
        public async Task SendInformationWithFilter_ReturnAmount()
        {
            //Arrange and Act
            var response = await _wordCountProcessor.GetAmountAsync(_requestWords);
            
            //Assert
            _ = response.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task SendInformationWithout_ReturnAmount()
        {
            //Arrange
            _requestWords.Filter = string.Empty;

            //Act
            var response = await _wordCountProcessor.GetAmountAsync(_requestWords);

            //Assert
            _ = response.Count.Should().BeGreaterThan(0);
        }
    }
}
