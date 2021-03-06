﻿using System;
using Pacman;
using Xunit;

namespace PacmanTests
{
    public class BlockTest
    {
        public BlockTest()
        {
        }

        [Fact]
        public void ShouldReturnTrue_WhenTopOfSquareIsAWall()
        {
            var square = new Block(true, true, false, false);
            var result = square.IsTopBorderAWall();

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalse_WhenTopOfSquareIsNotAWall()
        {
            var square = new Block(false, true, false, false);
            var result = square.IsTopBorderAWall();

            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrue_WhenRightOfSquareIsAWall()
        {
            var square = new Block(false, true, false, false);
            var result = square.IsRightBorderAWall();

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnTrue_WhenBottomOfSquareIsAWall()
        {
            var square = new Block(false, true, true, false);
            var result = square.IsBottomBorderAWall();

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnTrue_WhenLeftOfSquareIsAWall()
        {
            var square = new Block(false, true, true, true);
            var result = square.IsLeftBorderAWall();

            Assert.True(result);
        }
    }
}