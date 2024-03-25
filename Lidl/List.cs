using System;
using System.Collections.Generic;

public class Item
{
    public string id { get; set; }
    public DateTime date { get; set; }
    public double totalAmount { get; set; }
    public int articlesCount { get; set; }
    public int returnsCount { get; set; }
    public int couponsUsedCount { get; set; }
    public string store { get; set; }
    public bool isHtml { get; set; }
    public bool hasHtmlDocument { get; set; }
    public bool hasVendor { get; set; }
    public object vendor { get; set; }
}

public class RootObject
{
    public int page { get; set; }
    public int size { get; set; }
    public int totalCount { get; set; }
    public List<Item> items { get; set; }
}

public class Barcode
{
    public string data { get; set; }
    public string format { get; set; }
}

public class Ticket
{
    public string id { get; set; }
    public DateTime date { get; set; }
    public Barcode barcode { get; set; }
    public string htmlPrintedReceipt { get; set; }
}

public class Root
{
    public Ticket ticket { get; set; }
}