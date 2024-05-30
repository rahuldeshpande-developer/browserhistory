using Xunit;
using BrowserNavigationHistory.Controllers;
using BrowserNavigationHistory.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BrowserNavigationHistory.UnitTests;

public class BrowserHistoryControllerTests
{
    private List<HistoryItem> TestData()
    {
        return new List<HistoryItem>()
        {
            new HistoryItem()
            {
                Id = 0,
                Url = "www.youtube.com",
                Title = "Youtube",
                TimeStamp = DateTime.Now.AddDays(-2)
            },
            new HistoryItem()
            {
                Id = 1,
                Url = "www.linkedin.com",
                Title = "LinkedIn",
                TimeStamp = DateTime.Now.AddDays(-2).AddHours(5)
            },
            new HistoryItem()
            {
                Id = 2,
                Url = "www.google.com",
                Title = "Google",
                TimeStamp = DateTime.Now.AddDays(1).AddMonths(-4)
            }
        };
    }

    [Fact]
    public void Get_Returns_Correct_Number_Of_Items()
    {
        // Arrange
        List<HistoryItem> testData = TestData();
        Mock<IHistoryItemRepository> repo = new Mock<IHistoryItemRepository>();
        repo.Setup(r => r.Get()).Returns(testData);
        BrowserHistoryController controller = new BrowserHistoryController(repo.Object);

        // Act
        ActionResult<IEnumerable<HistoryItem>> actionResult = controller.Get();        
        
        // Assert
        OkObjectResult? result = actionResult.Result as OkObjectResult;
        Assert.NotNull(result);
        IEnumerable<HistoryItem>? items = result.Value as IEnumerable<HistoryItem>;
        Assert.NotNull(items);
        Assert.Equal(testData.Count, items.Count());
    }

    [Fact]
    public void Get_By_Id_Returns_Correct_Item()
    {
        // Arrange
        var testData = TestData();
        var repo = new Mock<IHistoryItemRepository>();
        repo.Setup(r => r.GetById(2)).Returns(testData[2]);
        BrowserHistoryController controller = new BrowserHistoryController(repo.Object);

        // Act
        var actionResult = controller.GetById(2);
        var result = actionResult.Result as OkObjectResult;
        var item = result.Value as HistoryItem;

        // Assert
        Assert.Equal(2, item.Id);
    }

    [Fact]
    public void Get_By_Id_Bad_Id_Returns_Not_Found()
    {
        // Arrange
        var testData = TestData();
        var repo = new Mock<IHistoryItemRepository>();
        repo.Setup(r => r.GetById(2)).Returns(() => { return null;});
        BrowserHistoryController controller = new BrowserHistoryController(repo.Object);

        // Act
        var actionResult = controller.GetById(2);

        // Assert
        Assert.True(actionResult.Result is NotFoundObjectResult);
    }


    [Fact]
    public void Bad_Id_Update_Throws_Not_Found()
    {
        // Arrange
        var testData = TestData();
        var repo = new Mock<IHistoryItemRepository>();
        repo.Setup(r => r.Get()).Returns(testData);
        var newItem = new HistoryItem()
        {
            Id = 11,
            Title = "www.Facebook.com",
            Url = "www.facebook.com",
            TimeStamp = DateTime.Now
        };

        // Act
        var controller = new BrowserHistoryController(repo.Object);
        var result = controller.Update(newItem);

        // Assert       
        Assert.True(result is NotFoundObjectResult);
    }

    [Fact]
    public void Update_Succeeds_With_Valid_Input()
    {
        // Arrange
        var originalItem = TestData()[0];
        var updatedItem = TestData()[1];
        originalItem.Id = updatedItem.Id;
        var repo = new Mock<IHistoryItemRepository>();
        repo.Setup(r => r.GetById(originalItem.Id)).Returns(originalItem);

        // Act
        var controller = new BrowserHistoryController(repo.Object);
        ActionResult result = controller.Update(updatedItem);

        // Assert
        StatusCodeResult? statusCodeResult = result as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(202, statusCodeResult.StatusCode);
    }

    
    [Fact]
    public void Create_Succeeds()
    {
        // Arrange
        HistoryItem historyItem = TestData()[0];
        var repo = new Mock<IHistoryItemRepository>();
        var controller = new BrowserHistoryController(repo.Object);
        repo.Setup(r => r.Create(It.IsAny<HistoryItem>()))
            .Callback((HistoryItem item) => {});

        // Act
        ActionResult<HistoryItem> result = controller.Create(historyItem);

        // Assert
        Assert.True(result.Result is CreatedResult);
    }

    [Fact]
    public void Delete_Returns_Correct_Status_Code()
    {
        // Arrange
        HistoryItem historyItem = TestData()[0];
        Mock<IHistoryItemRepository> repo = new Mock<IHistoryItemRepository>();
        BrowserHistoryController controller = new BrowserHistoryController(repo.Object);
        repo.Setup(r => r.Create(It.IsAny<HistoryItem>()))
            .Callback((HistoryItem item) => {});

        // Act
        ActionResult result = controller.Delete(historyItem.Id);

        // Assert
        StatusCodeResult? statusCodeResult = result as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }
}
