using lab6.Model;
using lab6;

var v1 = new Tomato(3.5);
var v2 = new Cucumber(1.5);
var v3 = new Lettuce(0.5);
var v4 = new Tomato(3.5);
var v5 = new Tomato(0.5);
var v6 = new Onion(5);

var ls = new LinkedSet(v1, v2, v3);

foreach(var item in ls)
{
    Console.WriteLine(item);
}

Console.WriteLine(ls.Count == 3);
ls.Add(v4);
Console.WriteLine(ls.Count == 4);
ls.Remove(v1);
Console.WriteLine(ls.Count == 3);

var ls2 = new LinkedSet(v2);
ls.ExceptWith(ls2);
Console.WriteLine(ls.Count == 2);

ls.Clear();
Console.WriteLine(ls.Count == 0);


