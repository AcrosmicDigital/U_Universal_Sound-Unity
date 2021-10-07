using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Universal.Sound;
using System.Linq;

public class StaticFunctionsTests
{
    [Test]
    public void Reverse()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        // Shuffle the items
        var reversedItems = items.Reverse().ToArray();

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(items);
        Debug.Log("Shuffled: " + reversedItems);
        StaticFunctionsCopy.PrintArray(reversedItems);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Shuffle()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        // Shuffle the items
        var shuffleItems = items.Shuffle();

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(items);
        Debug.Log("Shuffled: " + shuffleItems);
        StaticFunctionsCopy.PrintArray(shuffleItems);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ToQueue()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        // Shuffle the items
        var queue = items.ToQueue();

        // Dequeue two
        queue.Dequeue();
        queue.Dequeue();

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(items);
        Debug.Log("Shuffled: " + queue);
        StaticFunctionsCopy.PrintArray(queue);
    }


    // A Test behaves as an ordinary method
    [Test]
    public void ToStack()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        // Shuffle the items
        var queue = items.ToStack();

        // Dequeue two
        queue.Pop();
        queue.Pop();

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(items);
        Debug.Log("Shuffled: " + queue);
        StaticFunctionsCopy.PrintArray(queue);
    }


    // A Test behaves as an ordinary method
    [Test]
    public void JumpQueue()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        // Shuffle the items
        var queue = items.ToQueue();
        var queueJumped = queue.Jump(3);

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(queue);
        Debug.Log("Shuffled: ");
        StaticFunctionsCopy.PrintArray(queueJumped);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void JumpQueue_WithShortQueue()
    {
        // Create a enumerator
        var items = new int[] { 1, 2, 3, };

        // Shuffle the items
        var queue = items.ToQueue();
        var queueJumped = queue.Jump(5);

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(queue);
        Debug.Log("Shuffled: ");
        StaticFunctionsCopy.PrintArray(queueJumped);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void JumpQueue_WithEmptyQueue()
    {
        // Create a enumerator
        var items = new int[] { };

        // Shuffle the items
        var queue = items.ToQueue();
        var queueJumped = queue.Jump(5);

        // Print the results
        Debug.Log("Original: ");
        StaticFunctionsCopy.PrintArray(queue);
        Debug.Log("Shuffled: ");
        StaticFunctionsCopy.PrintArray(queueJumped);
    }

    [Test]
    public void RandomInt()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(StaticFunctionsCopy.RandomInt(0,4));
        }
    }

    [Test]
    public void RandomFloat()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(StaticFunctionsCopy.RandomFloat(0f, 4f));
        }
    }

    [Test]
    public void MinInt()
    {
        Assert.IsTrue(StaticFunctionsCopy.MinInt(5, 0) == 5); // = 5
        Assert.IsTrue(StaticFunctionsCopy.MinInt(1, 1) == 1); // = 1
        Assert.IsTrue(StaticFunctionsCopy.MinInt(-3, 0) == 0); // = 0
        Assert.IsTrue(StaticFunctionsCopy.MinInt(-5, 2) == 2); // = 2
        Assert.IsTrue(StaticFunctionsCopy.MinInt(-5, -2) == -2); // = -2
    }


    [Test]
    public void MinMaxFloat()
    {
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(5.12f, 0f, 10f) == 5.12f); // = 5
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(1.23f, 1.23f, 1.23f) == 1.23f); // = 1
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-3.22f, 0, 2f) == 0); // = 0
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-5.44f, 2, 3) == 2); // = 2
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-5.12f, -2, 3) == -2); // = -2
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-3.22f, -4f, -3.5f) == -3.5f); // = 0
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-5.44f, -20, -13) == -13); // = 2

        // Inverted
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(5.12f, 10f, 0f) == 5.12f); // = 5
        Assert.IsTrue(StaticFunctionsCopy.MinMaxFloat(-5.12f, 3, -2) == -2); // = -2
    }

}
