using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Tree;

namespace ViscoScrapeUnitTests {
	[TestClass]
	public class TreeTest {

		public Tree MyTree;

		[TestMethod]
		public void ConstructionTest() {
			MyTree = new Tree(new Node("2"));

			var node7 = MyTree.RootNode.AddChildNode(new Node("7"));
			var node5 = MyTree.RootNode.AddChildNode(new Node("9"));
			var node2 = node7.AddChildNode(new Node("2"));
			var node6 = node7.AddChildNode(new Node("6"));
			var node9 = node5.AddChildNode(new Node("9"));
			var node3 = node6.AddChildNode(new Node("3"));
			var node1 = node6.AddChildNode(new Node("1"));
			var node4 = node9.AddChildNode(new Node("4"));

			MyTree.Start();

			var result = MyTree.NavigatedNodeList;
			foreach (Node node in result) {
				LogHelper.Debug("Node: " + node.Name);
			}
			var expected = new List<Node> {MyTree.RootNode, node2, node3, node1, node6, node7, node4, node9, node5};
			Assert.AreEqual(expected, result);
		}
	}
}
