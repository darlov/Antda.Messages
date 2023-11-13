using System.Collections;
using Antda.Messages.Core.Helpers;
using Antda.Messages.Core.Tests.TestData;

namespace Antda.Messages.Core.Tests.Helpers;

public class TypeHelperTests
{
  [Fact]
  public void FindBaseTypes_WithNonGeneric()
  {
    var types = TypeHelper.FindTypes(typeof(ClassWithNonGenericClass), typeof(IMessage));
    Assert.Equal(types, new[] { typeof(IMessage) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericInterface), typeof(IMessage));
    Assert.Equal(types, new[] { typeof(IMessage) });
  }
  
  [Fact]
  public void FindBaseTypes_WithGeneric()
  {
    var types = TypeHelper.FindTypes(typeof(ClassWithGenericClass), typeof(IMessage<string>));
    Assert.Equal(types, new[] { typeof(IMessage<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass), typeof(IMessage<>));
    Assert.Equal(types, new[] { typeof(IMessage<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass), typeof(Message<>));
    Assert.Equal(types, new[] { typeof(Message<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass), typeof(Message<string>));
    Assert.Equal(types, new[] { typeof(Message<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericMultipleInterface), typeof(IMessage<>));
    Assert.Equal(types, new[] { typeof(IMessage<string>), typeof(IMessage<int>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericMultipleInterface), typeof(IMessage<int>));
    Assert.Equal(types, new[] { typeof(IMessage<int>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericMultipleInterface), typeof(IMessage<string>));
    Assert.Equal(types, new[] { typeof(IMessage<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass3), typeof(IMessage<string>));
    Assert.Equal(types, new[] { typeof(IMessage<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass3), typeof(Message<string>));
    Assert.Equal(types, new[] { typeof(Message<string>) });
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass3), typeof(Message<int>));
    Assert.Empty(types);
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass3), typeof(IMessage<int>));
    Assert.Empty(types);
    
    types = TypeHelper.FindTypes(typeof(ClassWithGenericClass3), typeof(ICollection));
    Assert.Empty(types);
  }
}