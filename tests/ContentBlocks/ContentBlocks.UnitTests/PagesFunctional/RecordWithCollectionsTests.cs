using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChillSite.ContentBlocks.UnitTests.PagesFunctional;

public class RecordWithCollectionsTests
{
    [Fact]
    public void Test()
    {
        var order = new Order(
            "Order 1")
        {
            Lines =
            [
                new OrderLine("Product 1", 10),
                new OrderLine("Product 2", 5)
            ]
        };

        var updatedOrder1 = order with
        {
            Name = order.Name + " Updated"
        };
        
        updatedOrder1.AddLine(new OrderLine("Product 3", 100));
        
        var updatedOrder2 = updatedOrder1 with { Name = "Updated Order" };
    }
    
    // Order
    public class OrderAggregate
    {
        // OrderDetails
        public Order Order { get; private set; }
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        public OrderAggregate(Order order, IEnumerable<OrderLine> orderLines)
        {
            Order = order;
            _orderLines.AddRange(orderLines);
        }
        
        public void AddLine(OrderLine line)
        {
            _orderLines.Add(line);
        }
        
        private readonly List<OrderLine> _orderLines = [];
    }

    public record Order(
        string Name)
    {
        public IReadOnlyCollection<OrderLine> Lines
        {
            get => _orderLines;
            init => _orderLines = [.. value];
        }
        
        public void AddLine(OrderLine line)
        {
            _orderLines.Add(line);
        }
        
        private readonly List<OrderLine> _orderLines = [];
    }

    public record OrderLine(string ProductName, decimal Cost);
    
    public class PageContentBlocksAggregate
    {
        public Page Page { get; private set; }
        public IReadOnlyDictionary<PageContainer, ContentBlock[]> ContentBlocks => _contentBlocks;

        public PageContentBlocksAggregate(
            Page page,
            IEnumerable<ContentBlock> contentBlocks)
        {
            Page = page;

            _contentBlocks = contentBlocks
                .GroupBy(x => x.PageContainer)
                .ToDictionary(x => x.Key, x => x.ToArray());
        }

        private readonly Dictionary<PageContainer, ContentBlock[]> _contentBlocks;
    }

    public record Page(
        Guid PageId,
        string Title,
        string? Description);
    
    public record ContentBlock(
        Guid ContentBlockId,
        PageContainer PageContainer);
    
    public record PageContainer(string Name);
}