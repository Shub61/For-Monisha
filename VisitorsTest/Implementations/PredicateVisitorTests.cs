namespace Expressions.VisitorsTest.Implementations
{
    public class PredicateVisitorTests: TestBaseClass
    {
        [TestClass]
        public class PredicateVisitorTests: TestBaseClass
        {
            #region Abs tests

            [DataRow(10, 10)]
            [DataRow(-10, 10)]
            [DataRow(10L, 10L)]
            [DataRow(-10L, 10L)]
            [DataRow(10.1f, 10.1f)]
            [DataRow(-10.1f, 10.1f)]
            [DataRow(10.1d, 10.1d)]
            [DataRow(-10.1d, 10.1d)]

            public void AbsExpression_WithNumericInputs_Produces_ExpectedResult(object operand, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(operand)),
                    Expression.Const(result)
                );
                expression.Accept(_visitor);
                var predicate = _visitor.Get();
                Assert.IsTrue(predicate(null));
            }

            [DataRow("10", 10)]
            [DataRow("-10.1", 10.1f)]

            public void AbsExpression_WithNumericType_And_StringAsInput_Produces_ExpectedResult(object operand, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(ExpressionType.Numeric, operand)),
                    Expression.Const(result)
                );
                expression.Accept(_visitor);
                var predicate = _visitor.Get();
                Assert.IsTrue(predicate(null));
            }

            [DataRow("10", 10)]
            [DataRow("-10.1", 10.1f)]
            public void AbsExpression_WithAnyType_And_StringAsInput_Produces_ExpectedResult(object operand, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(ExpressionType.Numeric, operand)),
                    Expression.Const(result)
                );
                expression.Accept(_visitor);
                var predicate = _visitor.Get();
                Assert.IsTrue(predicate(null));
            }

            [TestMethod]
            public void AbsExpression_WithNegativeDecimal_Produces_ExpectedResult(object operand, object result)
            {
                decimal value = -10m;
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(value)),
                    Expression.Const(Math.Abs(value))
                );
                expression.Accept(_visitor);
                var predicate = _visitor.Get();
                Assert.IsTrue(predicate(null));
            }

            [TestMethod]
            public void AbsExpression_WithPositiveDecimal_Produces_ExpectedResult()
            {
                decimal value = 10m;
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(value)),
                    Expression.Const(Math.Abs(value))
                );
                expression.Accept(_visitor);
                var predicate = _visitor.Get();
                Assert.IsTrue(predicate(null));
            }

            [DataRow(-10, 10)]
            [DataRow(-20L, 20L)]
            [DataRow(-30.3f, 30.3f)]
            [DataRow(-40.4d, 40.4d)]
            public void AbsExpression_Produces_ExpectedResult(object input, object result)
            {                
                var expression = Expression.AreEqual
                (
                    Expression.Abs(Expression.Const(ExpressionType.Numeric, input)),
                    Expression.Const(ExpressionType.Numeric, result)
                );                
                AssertIsTrue(expression);
            }

            #endregion Abs
            #region Add tests
            [DataTestMethod]
            [DataRow(1, 2, 1 + 2)]
            [DataRow(1L, 2L, 1L + 2L)]
            [DataRow(1f, 2f, 1f + 2f)]
            [DataRow(1d, 2d, 1d + 2d)]

            public void AddExpression_WithNumericss_NotDecimals_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Add(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);
            }

            [DataTestMethod]
            [DataRow(1, 2, 1 + 2)]
            [DataRow(1L, 2L, 1L + 2L)]
            [DataRow(1f, 2f, 1f + 2f)]
            [DataRow(1d, 2d, 1d + 2d)]

            public void AddExpression_WithNullableNumericss_NotDecimals_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Add(Expression.Const(ExpressionType.Numeric, UpliftToNullable(left)), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);

                _visitor.Reset();
                expression = Expression.AreEqual
                (
                    Expression.Add(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, UpliftToNullable(right))),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);
            }

            [TestMethod]
            public void AddExpression_WithDecimals_Produces_ExpectedResult()
            {
                var left = 1.1m;
                var right = 2.2m;
                var expression = Expression.AreEqual
                (
                    Expression.Add(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);

                _visitor.Reset();
                expression = Expression.AreEqual
                (
                    Expression.Add(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, left + right)
                );
                AssertIsTrue(expression);
            }
            #endregion Add

            #region Divide tests
            [DataTestMethod]
            [DataRow(10, 2, 5)]
            [DataRow(10L, 2L, 5L)]
            [DataRow(10.1f, 2f, 10.1f / 2f)]
            [DataRow(10.1d, 2d, 10.1d / 2d)]

            public void DivideExpression_WithNumericss_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Divide(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion Divide

            #region Multiply tests
            [DataTestMethod]
            [DataRow(10, 2, 10 * 2)]
            [DataRow(10L, 2L, 10L * 2L)]
            [DataRow(10.1f, 2f, 10.1f * 2f)]
            [DataRow(10.1d, 2d, 10.1d * 2d)]

            public void MultiplyExpression_WithNumericss_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Multiply(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion Multiply

            #region Modulo tests
            [DataTestMethod]
            [DataRow(10, 2, 10 % 2)]
            [DataRow(10, 3, 10 % 3)]
            [DataRow(10L, 2L, 10L % 2L)]
            [DataRow(10L, 3L, 10L % 3L)]

            public void ModuloExpression_WithNumerics_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Modulo(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion Modulo

            #region And tests
            [DataTestMethod]
            [DataRow(true, true, true)]
            [DataRow(true, false, false)]
            [DataRow(false, true, false)]
            [DataRow(false, false, false)]

            public void AndExpression_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Modulo(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion And

            #region Or tests
            [DataTestMethod]
            [DataRow(true, true, true)]
            [DataRow(true, false, true)]
            [DataRow(false, true, true)]
            [DataRow(false, false, false)]

            public void OrExpression_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Modulo(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Numeric, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion Or

            #region Not tests
            [DataTestMethod]
            [DataRow(true, false)]
            [DataRow(false, true)]            

            public void NotExpression_Produces_ExpectedResult(object left, object right, object result)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Not(Expression.Const(ExpressionType.Numeric, left), Expression.Const(ExpressionType.Numeric, right)),
                    Expression.Const(ExpressionType.Bool, result)
                );
                AssertIsTrue(expression);                                
            }
            #endregion Not

            #region AreEqual tests
            [DataTestMethod]
            [DataRow(1, 1)]
            [DataRow(1L, 1L)]    
            [DataRow(1.1f, 1.1f)]
            [DataRow(1.1d, 1.1d)]

            public void AreEqualExpression_Produces_ExpectedResult(object left, object right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Not(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }

            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void AreEqualExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }

            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void AreEqualExpression_WithNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }

            [DataTestMethod]
            [DataRow(1, 1)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void AreEqualExpression_WithNullableNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, UpliftToNullable(left)),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);

                _visitor.Reset();

                expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, UpliftToNullable(right))
                );
                AssertIsTrue(expression);                             
            }

            [DataTestMethod]
            [DataRow(1, 1)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void AreEqualExpression_WithNullableNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, UpliftToNullable(left)),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);

                _visitor.Reset();

                expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, UpliftToNullable(right))
                );
                AssertIsFalse(expression);                             
            }                        

            [TestMethod]
            public void AreEqualExpression_WithDecimals_Returns_True()
            {
                var left = 1.1m;
                var right = 1.1m;
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                             
            }

            [TestMethod]
            public void AreEqualExpression_WithDecimals_Returns_False()
            {
                var left = 1.1m;
                var right = 1.1m;
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                             
            }

            [TestMethod]
            public void AreEqualExpression_WithDateTime_Returns_True()
            {
                var left = DateTime.Now;
                var right = left;
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);                             
            }

            [TestMethod]
            public void AreEqualExpression_WithDateTime_Returns_False()
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(1);
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);                             
            }

            [DataTestMethod]
            [DataRow(true, true)]
            [DataRow(false, false)]
            public void AreEqualExpression_WithBool_Returns_True(bool left, bool right)
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(1);
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Bool, left),
                    Expression.Const(ExpressionType.Bool, right)
                );
                AssertIsFalse(expression);                             
            }

            [DataTestMethod]
            [DataRow(true, false)]
            [DataRow(false, true)]
            public void AreEqualExpression_WithBool_Returns_False(bool left, bool right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.Bool, left),
                    Expression.Const(ExpressionType.Bool, right)
                );
                AssertIsFalse(expression);                             
            }

            [DataTestMethod]
            [DataRow("ABC", "ABC")]
            [DataRow(null, null)]
            public void AreEqualExpression_WithString_Returns_True(bool left, bool right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.String, left),
                    Expression.Const(ExpressionType.String, right)
                );
                AssertIsFalse(expression);                             
            }

            [DataTestMethod]
            [DataRow("ABC", "ABCD")]
            [DataRow("ABCD", "ABC")]
            [DataRow("ABC", null)]
            [DataRow(null, "ABC")]
            public void AreEqualExpression_WithString_Returns_False(bool left, bool right)
            {
                var expression = Expression.AreEqual
                (
                    Expression.Const(ExpressionType.String, left),
                    Expression.Const(ExpressionType.String, right)
                );
                AssertIsFalse(expression);                             
            }                                                                                            
            #endregion AreEqual

            #region AreNotEqual tests
            [DataTestMethod]
            [DataRow(1, 1)]
            [DataRow(1L, 1L)]    
            [DataRow(1.1f, 1.1f)]
            [DataRow(1.1d, 1.1d)]

            public void AreNotEqualExpression_Numerics_Returns_False(object left, object right)
            {
                var expression = Expression.AreNotEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }

            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void AreNotEqualExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.AreNotEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }                                                                                                        
            #endregion AreNotEqual

            #region GreaterThan tests
            [DataTestMethod]
            [DataRow(2, 1)]
            [DataRow(2L, 1L)]    
            [DataRow(2.2f, 1.1f)]
            [DataRow(2.2d, 1.1d)]

            public void GreaterThanExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.GreaterThan
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            } 

            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1, 1)]    
            [DataRow(1L, 2L)]
            [DataRow(1L, 1L)]
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1f, 1.1f)]
            [DataRow(1.1d, 2.2d)]
            [DataRow(1.1d, 1.1d)]

            public void GreaterThanExpression_WithNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.GreaterThan
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }     

            [TestMethod]
            public void GreaterThanExpression_WithDateTime_Returns_True()
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(-1);
                var expression = Expression.GreaterThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);                             
            }

            [TestMethod]
            public void GreaterThanExpression_WithDateTime_Returns_False()
            {
                var left = DateTime.Now;
                var right = left;
                var expression = Expression.GreaterThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);             

                _visitor.Reset();

                right = left.AddMilliseconds(1);
                expression = Expression.GreaterThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);             
            }
            #endregion GreaterThan

            #region GreaterThanOrEqual
            [DataTestMethod]
            [DataRow(2, 1)]
            [DataRow(1, 1)]    
            [DataRow(2L, 1L)]
            [DataRow(1L, 1L)]
            [DataRow(2.2f, 1.1f)]
            [DataRow(1.1f, 1.1f)]
            [DataRow(2.2d, 1.1d)]
            [DataRow(1.1d, 1.1d)]

            public void GreaterThanOrEqualExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.GreaterThanOrEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }

            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1L, 2L)]
            [DataRow(1.1f, 2.2f)]            
            [DataRow(1.1d, 2.2d)]

            public void GreaterThanOrEqualExpression_WithNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.GreaterThanOrEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFlase(expression);                                
            }

            [TestMethod]
            public void GreaterThanOrEqualExpression_WithDateTime_Returns_True()
            {
                var left = DateTime.Now;
                var right = left;
                var expression = Expression.GreaterThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);             

                _visitor.Reset();

                right = left.AddMilliseconds(-1);
                expression = Expression.GreaterThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);             
            }

            [TestMethod]
            public void GreaterThanOrEqualExpression_WithDateTime_Returns_False()
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(1);
                var expression = Expression.GreaterThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);                
            }
            #endregion GreaterThanOrEqual

            #region LessThan tests
            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1L, 2L)]    
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1d, 2.2d)]

            public void LessThanExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.LessThan
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            } 

            [DataTestMethod]
            [DataRow(2, 1)]
            [DataRow(1, 1)]    
            [DataRow(2L, 1L)]
            [DataRow(1L, 1L)]
            [DataRow(2.2f, 1.1f)]
            [DataRow(1.1f, 1.1f)]
            [DataRow(2.2d, 1.1d)]
            [DataRow(1.1d, 1.1d)]

            public void LessThanExpression_WithNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.LessThan
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }     

            [TestMethod]
            public void LessThanExpression_WithDateTime_Returns_True()
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(1);
                var expression = Expression.LessThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);                             
            }

            [TestMethod]
            public void LessThanExpression_WithDateTime_Returns_False()
            {
                var left = DateTime.Now;
                var right = left;
                var expression = Expression.LessThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);             

                _visitor.Reset();

                right = left.AddMilliseconds(-1);
                expression = Expression.LessThan
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);             
            }
            #endregion LessThan

            #region LessThanOrEqual
            [DataTestMethod]
            [DataRow(1, 2)]
            [DataRow(1, 1)]    
            [DataRow(1L, 2L)]
            [DataRow(1L, 1L)]
            [DataRow(1.1f, 2.2f)]
            [DataRow(1.1f, 1.1f)]
            [DataRow(1.1d, 2.2d)]
            [DataRow(1.1d, 1.1d)]

            public void LessThanOrEqualExpression_WithNumerics_Returns_True(object left, object right)
            {
                var expression = Expression.LessThanOrEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }

            [DataTestMethod]
            [DataRow(2, 1)]
            [DataRow(2L, 1L)]
            [DataRow(2.2f, 1.1f)]            
            [DataRow(2.2d, 1.1d)]

            public void LessThanOrEqualExpression_WithNumerics_Returns_False(object left, object right)
            {
                var expression = Expression.LessThanOrEqual
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFlase(expression);                                
            }

            [TestMethod]
            public void LessThanOrEqualExpression_WithDateTime_Returns_True()
            {
                var left = DateTime.Now;
                var right = left;
                var expression = Expression.LessThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);             

                _visitor.Reset();

                right = left.AddMilliseconds(1);
                expression = Expression.LessThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsTrue(expression);             
            }

            [TestMethod]
            public void LessThanOrEqualExpression_WithDateTime_Returns_False()
            {
                var left = DateTime.Now;
                var right = left.AddMilliseconds(-1);
                var expression = Expression.LessThanOrEqual
                (
                    Expression.Const(ExpressionType.DateTime, left),
                    Expression.Const(ExpressionType.DateTime, right)
                );
                AssertIsFalse(expression);                
            }
            #endregion LessThanOrEqual

            #region Like tests
            [DataTestMethod]
            [DataRow("ABCD", "ABCD")]
            [DataRow("ABCD", "AbcD")]
            [DataRow("ABCD", "ABC")]
            [DataRow("ABCD", "BC")]            

            public void LikeExpression_WithStrings_Returns_True(object left, object right)
            {
                var expression = Expression.Like
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsTrue(expression);                                
            }

            DataTestMethod]
            [DataRow("ABCD", "DCBA")]
            [DataRow("ABCD", "XYZY")]
            [DataRow("ABC", "ABCD")]
            [DataRow("BCD", "ABCD")]
            [DataRow("BC", "ABCD")]            

            public void LikeExpression_WithStrings_Returns_False(object left, object right)
            {
                var expression = Expression.Like
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }

            [TestMethod]
            public void LikeExpression_ConvertLeftOperand_And_Returns_True(object left, object right)
            {
                var expression = Expression.Like
                (
                    Expression.Const(12345),
                    Expression.Const("234")
                );
                AssertIsFalse(expression);                                
            }
            
            [DataRow("ABCD", null)]
            [DataRow(null, "ABCD")]
            [DataRow(null, null)]            

            public void LikeExpression_WithEitherNullOperand_Returns_False(object left, object right)
            {
                var expression = Expression.Like
                (
                    Expression.Const(ExpressionType.Numeric, left),
                    Expression.Const(ExpressionType.Numeric, right)
                );
                AssertIsFalse(expression);                                
            }
            #endregion Like

            #region Member tests
            public void MsExpressions_Properly_Handle_Sub_Properties()
            {
                var value = 5;
                var expression = Expression.AreEqual
                (
                    Expression.Member($"{nameof(TestMainClass.MainProperty)}.{nameof(TestMainClass.TestSubClass.IntSubProperty)}"),
                    Expression.Const(ExpressionType.Numeric, value)
                );

                var visitor = new PredicateVisitor<TestMainClass>();
                expression.Accept(visitor);
                var predicate = visitor.Get();
                var instance = new TestMainClass {MainProperty = new TestMainClass.TestSubClass {IntSubProperty = value}};

                Assert.IsTrue(predicate(instance));
            }
            #endregion Member

            #region  Performance pseudo tests
            public void SimplePerformanceTest()
            {
                const int count = 1000000;
                var expression1 = Expression.AreEqual
                (
                    Expression.Add(Expression.Member(nameof(MathTestsClass.IntProperty1)), Expression.Member(nameof(MathTestsClass.NullableInt1)),
                    Expression.Const(ExpressionType.Any, _instance.IntProperty1 + _instance.NullableInt1)
                );

                var expression2 = Expression.GreaterThanOrEqual
                (
                    Expression.Abs
                    (
                        Expression.Subtract(nameof(MathTestsClass.DoubleProperty1)), Expression.Member(nameof(MathTestsClass.DoubleProperty2)),
                    ),
                    Expression.Const(ExpressionType.Numeric, _instance.DoubleProperty2 - _instance.DoubleProperty1)
                );

                var expression = Expression.And
                (
                    expression1, expression2
                );

                expression.Accept(_visitor);
                var predicate = _visitor.Get();

                var watch = new StopWatch();
                watch.Start();
                var result = true;

                for(var i = 0; i < count; i++)
                {
                    result &= predicate(_instance);
                }

                watch.Stop();

                Debug.WriteLine($"Performance time took: {watch.ElapseMilliseconds} msec");
                Assert.IsTrue(result);
                Assert.IsTrue(watch.ElapseMilliseconds < 1000);                
            }
            #endregion Performance
        }
    }
}