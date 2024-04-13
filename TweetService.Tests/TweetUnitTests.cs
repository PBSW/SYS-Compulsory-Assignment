
using AutoMapper;
using FluentAssertions;
using Moq;

using Shared.Domain;

using Shared.Tweet.Dto;
using TweetService.Infrastructure;

public class TweetServiceTests
{
    private readonly Mock<ITweetRepository> _mockTweetRepository = new Mock<ITweetRepository>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private readonly TweetService.Service.TweetService _tweetService;

    public TweetServiceTests()
    {
        _tweetService = new TweetService.Service.TweetService(_mockTweetRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateTweet_ValidTweet_ReturnsTweetDTO()
    {
        // Arrange
        var tweetCreate = new TweetCreate { Content = "Hello World!", AuthorId = 1 };
        var tweet = new Tweet { Content = "Hello World!", AuthorId = 1, CreatedAt = DateTime.Now };
        var tweetDto = new TweetDTO { Content = "Hello World!", AuthorId = 1 };

        _mockMapper.Setup(m => m.Map<TweetCreate, Tweet>(It.IsAny<TweetCreate>())).Returns(tweet);
        _mockTweetRepository.Setup(repo => repo.Create(It.IsAny<Tweet>())).ReturnsAsync(tweet);
        _mockMapper.Setup(m => m.Map<Tweet, TweetDTO>(It.IsAny<Tweet>())).Returns(tweetDto);

        // Act
        var result = await _tweetService.CreateTweet(tweetCreate);

        // Assert
        result.Should().BeEquivalentTo(tweetDto);
    }

    [Fact]
    public async Task GetTweetsFromUser_ValidUserId_ReturnsListOfTweetDTO()
    {
        // Arrange
        int userId = 1;
        var tweets = new List<Tweet> { new Tweet { Content = "Test", AuthorId = 1 } };
        var tweetDtos = new List<TweetDTO> { new TweetDTO { Content = "Test", AuthorId = 1 } };

        _mockTweetRepository.Setup(repo => repo.AllFrom(userId)).ReturnsAsync(tweets);
        _mockMapper.Setup(m => m.Map<List<Tweet>, List<TweetDTO>>(It.IsAny<List<Tweet>>())).Returns(tweetDtos);

        // Act
        var result = await _tweetService.GetTweetsFromUser(userId);

        // Assert
        result.Should().BeEquivalentTo(tweetDtos);
    }

    [Fact]
    public async Task GetAllTweets_ReturnsListOfTweetDTO()
    {
        // Arrange
        var tweets = new List<Tweet> { new Tweet { Content = "Hello", AuthorId = 2 } };
        var tweetDtos = new List<TweetDTO> { new TweetDTO { Content = "Hello", AuthorId = 2 } };

        _mockTweetRepository.Setup(repo => repo.All()).ReturnsAsync(tweets);
        _mockMapper.Setup(m => m.Map<List<Tweet>, List<TweetDTO>>(It.IsAny<List<Tweet>>())).Returns(tweetDtos);

        // Act
        var result = await _tweetService.GetAllTweets();

        // Assert
        result.Should().BeEquivalentTo(tweetDtos);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsTrue()
    {
        // Arrange
        int tweetId = 100;
        _mockTweetRepository.Setup(repo => repo.Delete(tweetId)).ReturnsAsync(true);

        // Act
        var result = await _tweetService.Delete(tweetId);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Delete_InvalidId_ThrowsNullReferenceException(int invalidId)
    {
        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _tweetService.Delete(invalidId));
    }
}
