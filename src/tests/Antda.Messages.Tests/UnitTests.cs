namespace Antda.Messages.Tests;

public class UnitTests
{
  [Fact]
  public void Equal_WithDifferentVariables_ShouldBeTrue()
  {
    var left = Unit.Value;
    var right = Unit.Value;
    
    Assert.Equal(left, right);
    Assert.True(left == right);
    Assert.False(left != right);
  }
  
  [Fact]
  public void CompareTo_WithDifferentVariables_ShouldBeTrue()
  {
    const string ExceptionMessage = "Object must be of type Antda.Messages.Unit";
    
    var left = Unit.Value;
    var right = Unit.Value;

    Assert.Equal(0, left.CompareTo(right));
    Assert.Equal(1, left.CompareTo(null));

    var ex1 = Assert.Throws<ArgumentException>("other", () => left.CompareTo(new object()));
    Assert.Contains(ExceptionMessage, ex1.Message);
    
    var ex2 = Assert.Throws<ArgumentException>("other", () => left.CompareTo(string.Empty));
    Assert.Contains(ExceptionMessage, ex2.Message);
  }
  
  [Fact]
  public void GetHashCode_WithDifferentVariables_ShouldBeEqual()
  {
    var left = Unit.Value;
    var right = Unit.Value;

    Assert.Equal(left.GetHashCode(), right.GetHashCode());
  }
}