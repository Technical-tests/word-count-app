using AutoBogus;

using FluentAssertions;

using NSubstitute;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using word_count_app.api.Controllers;
using word_count_app.common.Helpers;
using word_count_app.common.Models;

using Xunit;

namespace word_count_app.test.api
{
    public class WordCountControllerTest
    {
        private IWordCountProcessor _wordCountProcessor;
        private readonly RequestWords _requestWords;
        private readonly Dictionary<string, ushort> _responseWords;
        private WordCountController _service;
        

        public WordCountControllerTest()
        {
            _requestWords = AutoFaker.Generate<RequestWords>();
            _responseWords = AutoFaker.Generate<Dictionary<string, ushort>>();
            _wordCountProcessor = Substitute.For<IWordCountProcessor>();

            _ = _wordCountProcessor.GetAmountAsync(Arg.Any<RequestWords>()).Returns(_responseWords);
            _service = new WordCountController(_wordCountProcessor);
        }

        [Fact]
        public void SendRequest_ReturnsException()
        {
            //Arrange and Act
            _wordCountProcessor = null;
            Action act = () => _service = new WordCountController(_wordCountProcessor);

            //Assert
            _= act.Should().Throw<ArgumentNullException>().Where(w => string.Equals(w.ParamName, "wordCountProcessor"));
        }

        [Fact]
        public async Task SendCorrectInformation_ReturnBadRequest()
        {
            //Arrange and Act
            var response = await _service.Count(new RequestWords());
            var result = ((Microsoft.AspNetCore.Mvc.ObjectResult)response);

            //Assert
            Assert.Equal(expected: 400, actual:result.StatusCode);
        }

        [Fact]
        public async Task SendCorrectInformation_ReturnCount()
        {
            //Arrange and Act
            var response = await _service.Count(_requestWords);
            var result = ((IDictionary<string, ushort>)(((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value));

            //Assert
            _ = result.Count.Should().BeGreaterThan(0);
        }
    }
}
