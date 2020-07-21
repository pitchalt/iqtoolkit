using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IQToolkit.Data.ClickHouse
{
    using IQToolkit.Data.Common;

    public class ClickHouseFormatter : SqlFormatter
    {
        private ClickHouseFormatter(QueryLanguage language)
            : base(language)
        {
        }

        public static new string Format(Expression expression)
        {
            return Format(expression, new ClickHouseLanguage());
        }

        public static string Format(Expression expression, QueryLanguage language)
        {
            ClickHouseFormatter formatter = new ClickHouseFormatter(language);
            formatter.Visit(expression);
            return formatter.ToString();
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            this.AddAliases(select.From);
            this.Write("SELECT ");
            if (select.IsDistinct)
            {
                this.Write("DISTINCT ");
            }
            this.WriteColumns(select.Columns);
            if (select.From != null)
            {
                this.WriteLine(Indentation.Same);
                this.Write("FROM ");
                this.VisitSource(select.From);
            }
            if (select.Where != null)
            {
                this.WriteLine(Indentation.Same);
                this.Write("WHERE ");
                this.VisitPredicate(select.Where);
            }
            if (select.GroupBy != null && select.GroupBy.Count > 0)
            {
                this.WriteLine(Indentation.Same);
                this.Write("GROUP BY ");
                for (int i = 0, n = select.GroupBy.Count; i < n; i++)
                {
                    if (i > 0)
                    {
                        this.Write(", ");
                    }
                    this.VisitValue(select.GroupBy[i]);
                }
            }
            if (select.OrderBy != null && select.OrderBy.Count > 0)
            {
                this.WriteLine(Indentation.Same);
                this.Write("ORDER BY ");
                for (int i = 0, n = select.OrderBy.Count; i < n; i++)
                {
                    OrderExpression exp = select.OrderBy[i];
                    if (i > 0)
                    {
                        this.Write(", ");
                    }
                    this.VisitValue(exp.Expression);
                    if (exp.OrderType != OrderType.Ascending)
                    {
                        this.Write(" DESC");
                    }
                }
            }
            if (select.Take != null)
            {
                this.WriteLine(Indentation.Same);
                this.Write("LIMIT ");
                if (select.Skip == null)
                {
                    this.Write("0");
                }
                else
                {
                    this.Write(select.Skip);
                }
                this.Write(", ");
                this.Visit(select.Take);
            }
            return select;
        }

        protected override void WriteColumns(System.Collections.ObjectModel.ReadOnlyCollection<ColumnDeclaration> columns)
        {
            if (columns.Count == 0)
            {
                this.Write("0");
                if (this.IsNested)
                {
                    this.Write(" AS ");
                    this.WriteColumnName("tmp");
                    this.Write(" ");
                }
            }
            else
            {
                base.WriteColumns(columns);
            }
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Member.DeclaringType == typeof(string))
            {
                switch (m.Member.Name)
                {
                    case "Length":
                        this.Write("LENGTH(");
                        this.Visit(m.Expression);
                        this.Write(")");
                        return m;
                }
            }
            else if (m.Member.DeclaringType == typeof(DateTime) || m.Member.DeclaringType == typeof(DateTimeOffset))
            {
                switch (m.Member.Name)
                {
                    case "Day":
                        this.Write("toDayOfMonth(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "Month":
                        this.Write("toMonth(toDateTime( ");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "Year":
                        this.Write("toYear(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "Hour":
                        this.Write("toHour(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "Minute":
                        this.Write("toMinute(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "Second":
                        this.Write("toSecond(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    /*  case "Millisecond":
                          this.Write("toDateTime('%f', ");
                          this.Visit(m.Expression);
                          this.Write(")");
                          return m;*/
                    case "DayOfWeek":
                        this.Write("toDayOfWeek(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write("))");
                        return m;
                    case "DayOfYear":
                        this.Write("toDayOfYear(toDateTime(");
                        this.Visit(m.Expression);
                        this.Write(") - 1)");
                        return m;
                }
            }
            return base.VisitMemberAccess(m);
        }
        
        protected override void WriteAggregateName(string aggregateName)
        {
            if (aggregateName == "Sum")
            {
                this.Write("sumWithOverflow");                    
            }
            else 
                base.WriteAggregateName(aggregateName);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(string))
            {
                switch (m.Method.Name)
                {
                    case "IndexOf":

                        if (m.Arguments.Count == 1)
                        {
                            this.Write("position(");
                            this.Visit(m.Object);
                            this.Write(", ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" ) -1");
                        }
                        else if (m.Arguments.Count == 2)
                        {
                            this.Write("position( substring(");
                            this.Visit(m.Object);
                            this.Write(", ");
                            this.Visit(m.Arguments[1]);
                            this.Write(", length(");
                            this.Visit(m.Object);
                            this.Write(")-");
                            this.Visit(m.Arguments[1]);
                            this.Write("+1),");
                            this.Visit(m.Arguments[0]);
                            this.Write(") -1 + ");
                            this.Visit(m.Arguments[1]);
                            this.Write("-1 ");
                            //position(substring("ContactName", n,length("ContactName") -n + 1), 'a') -1 + n 
                        }
                        else throw new Exception("that indexof in not supported, update VisitMethodCall in ClickHouseFormatter");

                        return m;

                    //+
                    case "StartsWith":

                        this.Write("startsWith(");
                        this.Visit(m.Object);
                        this.Write(", ");
                        this.Visit(m.Arguments[0]);
                        this.Write(" )");
                        return m;

                    //+
                    case "EndsWith":
                        this.Write("endsWith(");
                        this.Visit(m.Object);
                        this.Write(", ");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;
                    //+
                    case "Contains":
                        this.Write("position(");
                        this.Visit(m.Object);
                        this.Write(", ");
                        this.Visit(m.Arguments[0]);
                        this.Write(") <> 0");
                        return m;
                    case "Concat":
                        IList<Expression> args = m.Arguments;
                        if (args.Count == 1 && args[0].NodeType == ExpressionType.NewArrayInit)
                        {
                            args = ((NewArrayExpression)args[0]).Expressions;
                        }
                        for (int i = 0, n = args.Count; i < n; i++)
                        {
                            if (i > 0) this.Write(" || ");
                            this.Visit(args[i]);
                        }
                        return m;
                    case "IsNullOrEmpty":
                        this.Write("(");
                        this.Visit(m.Arguments[0]);
                        this.Write(" IS NULL OR ");
                        this.Visit(m.Arguments[0]);
                        this.Write(" = '')");
                        return m;
                    case "ToUpper":
                        this.Write("UPPER(");
                        this.Visit(m.Object);
                        this.Write(")");
                        return m;
                    case "ToLower":
                        this.Write("LOWER(");
                        this.Visit(m.Object);
                        this.Write(")");
                        return m;
                    case "Replace":
                        this.Write("REPLACE(");
                        this.Visit(m.Object);
                        this.Write(", ");
                        this.Visit(m.Arguments[0]);
                        this.Write(", ");
                        this.Visit(m.Arguments[1]);
                        this.Write(")");
                        return m;
                    case "Substring":
                        this.Write("SUBSTR(");
                        this.Visit(m.Object);
                        this.Write(", ");
                        this.Visit(m.Arguments[0]);
                        this.Write(" + 1, ");
                        if (m.Arguments.Count == 2)
                        {
                            this.Visit(m.Arguments[1]);
                        }
                        else
                        {
                            this.Write("8000");
                        }
                        this.Write(")");
                        return m;
                    case "Remove":
                        if (m.Arguments.Count == 1)
                        {
                            this.Write("SUBSTR(");
                            this.Visit(m.Object);
                            this.Write(", 1, ");
                            this.Visit(m.Arguments[0]);
                            this.Write(")");
                        }
                        else
                        {
                            this.Write("SUBSTR(");
                            this.Visit(m.Object);
                            this.Write(", 1, ");
                            this.Visit(m.Arguments[0]);
                            this.Write(") + SUBSTR(");
                            this.Visit(m.Object);
                            this.Write(", ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" + ");
                            this.Visit(m.Arguments[1]);
                            this.Write(")");
                        }
                        return m;
                    case "Trim":
                        this.Write("TRIM(");
                        this.Visit(m.Object);
                        this.Write(")");
                        return m;
                }
            }
            else if (m.Method.DeclaringType == typeof(DateTime))
            {
                switch (m.Method.Name)
                {
                    case "op_Subtract":
                        if (m.Arguments[1].Type == typeof(DateTime))
                        {
                            this.Write("DATEDIFF(");
                            this.Visit(m.Arguments[0]);
                            this.Write(", ");
                            this.Visit(m.Arguments[1]);
                            this.Write(")");
                            return m;
                        }
                        break;

                    case "AddYears":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" year");
                            return m;
                        }
                        break;

                    case "AddMonths":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" month");
                            return m;
                        }
                        break;

                    case "AddDays":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" day");
                            return m;
                        }
                        break;
                    case "AddHours":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" hour");
                            return m;
                        }
                        break;
                    case "AddMinutes":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" minute");
                            return m;
                        }
                        break;
                    case "AddSeconds":
                        if (m.Object.Type == typeof(DateTime))
                        {
                            this.Visit(m.Object);
                            this.Write(" + interval ");
                            this.Visit(m.Arguments[0]);
                            this.Write(" second");
                            return m;
                        }
                        break;
                }
            }
            else if (m.Method.DeclaringType == typeof(Decimal))
            {
                switch (m.Method.Name)
                {
                    case "Add":
                    case "Subtract":
                    case "Multiply":
                    case "Divide":
                    case "Remainder":
                        this.Write("(");
                        this.VisitValue(m.Arguments[0]);
                        this.Write(" ");
                        this.Write(GetOperator(m.Method.Name));
                        this.Write(" ");
                        this.VisitValue(m.Arguments[1]);
                        this.Write(")");
                        return m;
                    case "Negate":
                        this.Write("-");
                        this.Visit(m.Arguments[0]);
                        this.Write("");
                        return m;
                    case "Round":
                        if (m.Arguments.Count == 1)
                        {
                            this.Write("ROUND(");
                            this.Visit(m.Arguments[0]);
                            this.Write(", 0)");
                            return m;
                        }
                        else if (m.Arguments.Count == 2 && m.Arguments[1].Type == typeof(int))
                        {
                            this.Write("ROUND(");
                            this.Visit(m.Arguments[0]);
                            this.Write(", ");
                            this.Visit(m.Arguments[1]);
                            this.Write(")");
                            return m;
                        }
                        break;
                    case "Floor":
                        this.Write("floor(");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;
                    case "Truncate":
                        this.Write("truncate(");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;
                }
            }
            else if (m.Method.DeclaringType == typeof(Math))
            {
                switch (m.Method.Name)
                {
                    case "Abs":
                    case "Acos":
                    case "Asin":
                    case "Atan":
                    case "Cos":
                    case "Exp":
                    case "Log10":
                    case "Sin":
                    case "Tan":
                    case "Sqrt":
                    case "Sign":
                        this.Write(m.Method.Name.ToUpper());
                        this.Write("(");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;
                    case "Atan2":
                        this.Write("ATN2(");
                        this.Visit(m.Arguments[0]);
                        this.Write(", ");
                        this.Visit(m.Arguments[1]);
                        this.Write(")");
                        return m;
                    case "Log":
                        if (m.Arguments.Count == 1)
                        {
                            goto case "Log10";
                        }
                        break;
                    case "Pow":
                        this.Write("POWER(");
                        this.Visit(m.Arguments[0]);
                        this.Write(", ");
                        this.Visit(m.Arguments[1]);
                        this.Write(")");
                        return m;
                    case "Round":
                        if (m.Arguments.Count == 1)
                        {
                            this.Write("ROUND(");
                            this.Visit(m.Arguments[0]);
                            this.Write(", 0)");
                            return m;
                        }
                        else if (m.Arguments.Count == 2 && m.Arguments[1].Type == typeof(int))
                        {
                            this.Write("ROUND(");
                            this.Visit(m.Arguments[0]);
                            this.Write(", ");
                            this.Visit(m.Arguments[1]);
                            this.Write(")");
                            return m;
                        }
                        break;
                    case "Floor":
                        this.Write("floor(");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;
                    case "Truncate":
                        this.Write("truncate(");
                        this.Visit(m.Arguments[0]);
                        this.Write(")");
                        return m;

                }
            }
            if (m.Method.Name == "ToString")
            {
                // no-op
                this.Visit(m.Object);
                return m;
            }
            else if (!m.Method.IsStatic && m.Method.Name == "CompareTo" && m.Method.ReturnType == typeof(int) && m.Arguments.Count == 1)
            {


                this.Write("caseWithoutExpression(");
                this.Visit(m.Object);
                this.Write(" = ");
                this.Visit(m.Arguments[0]);
                this.Write(", 0, ");
                this.Visit(m.Object);
                this.Write(" < ");
                this.Visit(m.Arguments[0]);
                this.Write(", -1, 1)");
                return m;

                //select OrderID, caseWithoutExpression(OrderID = 1, 11, OrderID = 2, 12, 0) from temp_table

            }
            else if (m.Method.IsStatic && m.Method.Name == "Compare" && m.Method.ReturnType == typeof(int) && m.Arguments.Count == 2)
            {
                this.Write("caseWithoutExpression(");
                this.Visit(m.Arguments[0]);
                this.Write(" = ");
                this.Visit(m.Arguments[1]);
                this.Write(", 0, ");
                this.Visit(m.Arguments[0]);
                this.Write(" < ");
                this.Visit(m.Arguments[1]);
                this.Write(", -1, 1)");
                return m;

                ////select OrderID, caseWithoutExpression(OrderID = 1, 11, OrderID = 2, 12, 0) from temp_table

            }
            else if (m.Method.Name == "Contains")
            {
                this.Write("has([");
                this.Visit(m.Object);
                this.Write("], ");
                this.Visit(m.Arguments[0]);
                this.Write(")");
                return m;
            }
                return base.VisitMethodCall(m);
        }
        protected override Expression VisitUnary(UnaryExpression u)
        {
            string op = this.GetOperator(u);
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    if (u.Operand is IsNullExpression)
                    {
                        this.Visit(((IsNullExpression)u.Operand).Expression);
                        this.Write(" IS NOT NULL");
                    }
                    else if (IsBoolean(u.Operand.Type) || op.Length > 1)
                    {
                        this.Write(op);
                        this.Write(" ");
                        this.VisitPredicate(u.Operand);
                    }
                    else
                    {
                        this.Write("bitNot(");
                        this.Visit(u.Operand);
                        this.Write(")");
                    }
                    break;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    this.Write(op);
                    this.VisitValue(u.Operand);
                    break;
                case ExpressionType.UnaryPlus:
                    this.VisitValue(u.Operand);
                    break;
                case ExpressionType.Convert:
                    // ignore conversions for now
                    this.Visit(u.Operand);
                    break;
                default:
                    if (this.ForDebug)
                    {
                        this.Write(string.Format("?{0}?", u.NodeType));
                        this.Write("(");
                        this.Visit(u.Operand);
                        this.Write(")");
                        return u;
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
                    }
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Power)
            {
                this.Write("POWER(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.Coalesce)
            {
                this.Write("COALESCE(");
                this.VisitValue(b.Left);
                this.Write(", ");
                Expression right = b.Right;
                while (right.NodeType == ExpressionType.Coalesce)
                {
                    BinaryExpression rb = (BinaryExpression)right;
                    this.VisitValue(rb.Left);
                    this.Write(", ");
                    right = rb.Right;
                }
                this.VisitValue(right);
                this.Write(")");
                return b;
            }
            else if (b.NodeType == ExpressionType.LeftShift)
            {
                this.Write("bitShiftLeft(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(" )");
                return b;
            }

            else if (b.NodeType == ExpressionType.RightShift)
            {
                this.Write("bitShiftRight(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(" )");
                return b;
            }
            else if (b.NodeType == ExpressionType.And && (!this.IsBoolean(b.Left.Type)))
            {
                this.Write("bitAnd(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(") ");
                return b;
            }
            else if (b.NodeType == ExpressionType.Or && (!this.IsBoolean(b.Left.Type)))
            {
                this.Write("bitOr(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(") ");
                return b;
            }
            else if (b.NodeType == ExpressionType.ExclusiveOr && (!this.IsBoolean(b.Left.Type)))
            {
                this.Write("bitXor(");
                this.VisitValue(b.Left);
                this.Write(", ");
                this.VisitValue(b.Right);
                this.Write(") ");
                return b;
            }



            return base.VisitBinary(b);
        }

        protected override string GetOperator(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Add && b.Type == typeof(string))
            {
                return "||";
            }
            else
            {
                return base.GetOperator(b);
            }
        }

        protected override NewExpression VisitNew(NewExpression nex)
        {
            if (nex.Constructor.DeclaringType == typeof(DateTime))
            {
                if (nex.Arguments[0].NodeType == ExpressionType.Conditional)
                {

                    if (nex.Arguments.Count == 3)
                    {
                        this.Write("ToDate(concat(toString(");
                        this.Visit(nex.Arguments[0]);
                        this.Write("), '-', toString(");
                        this.Write(nex.Arguments[1]);
                        this.Write("), '-', toString(");
                        this.Write(nex.Arguments[2]);
                        this.Write(")))");
                        return nex;

                        //this.Write("parseDateTimeBestEffort(concat(toString(");
                        //this.Visit(nex.Arguments[0]);
                        //this.Write("), '-', toString(");
                        //this.Write(nex.Arguments[1]);
                        //this.Write("), '-', toString(");
                        //this.Write(nex.Arguments[2]);
                        //this.Write("), ' 00:00:00.Z' ))");
                        //return nex;
                    }
                    else if (nex.Arguments.Count == 6)
                    {
                        this.Write("parseDateTimeBestEffort(concat(toString(");
                        this.Visit(nex.Arguments[0]);
                        this.Write("), '-', toString(");
                        this.Write(nex.Arguments[1]);
                        this.Write("), '-', toString(");
                        this.Write(nex.Arguments[2]);
                        this.Write("), 'T', toString(");
                        this.Write(nex.Arguments[3]);
                        this.Write("), ':', toString(");
                        this.Write(nex.Arguments[4]);
                        this.Write("), ':', toString(");
                        this.Write(nex.Arguments[5]);
                        this.Write("), '.Z'))");
                        return nex;

                    }
                }
            }

            /*
             * select Max(toDate(concat(toString(if((t0."CustomerID" = 'ALFKI'), 1977, 1977)),'-0', toString(7), '-0', toString(6) ) ))
             * FROM "Customers" AS t0
             * WHERE (t0."CustomerID" = 'ALFKI')                     * 
             */


            else
            {
                this.Write("(");
                this.Visit(nex.Arguments[0]);
                this.Write(" || '-' || (caseWithoutExpression(");
                this.Visit(nex.Arguments[1]);
                this.Write(" < 10, '0' || ");
                this.Visit(nex.Arguments[1]);
                this.Write(" ELSE ");
                this.Visit(nex.Arguments[1]);
                this.Write(" END)");
                this.Write(" || '-' || (caseWithoutExpression( ");
                this.Visit(nex.Arguments[2]);
                this.Write(" < 10, '0' || ");
                this.Visit(nex.Arguments[2]);
                this.Write(" , ");
                this.Visit(nex.Arguments[2]);
                this.Write(")");
                this.Write(")");
                return nex;
            }


            return base.VisitNew(nex);
        }

        protected override Expression VisitValue(Expression expr)
        {
            if (IsPredicate(expr))
            {
                this.Write("if(");
                this.Visit(expr);
                this.Write(", 1, 0)");
                return expr;

            }
            return base.VisitValue(expr);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            if (this.IsPredicate(c.Test))
            {

                this.Write("if(");
                this.VisitPredicate(c.Test);
                this.Write(", ");
                this.VisitValue(c.IfTrue);
                Expression ifFalse = c.IfFalse;
                while (ifFalse != null && ifFalse.NodeType == ExpressionType.Conditional)
                {
                    ConditionalExpression fc = (ConditionalExpression)ifFalse;
                    this.Write(", ");
                    this.VisitPredicate(fc.Test);
                    this.Write(", ");
                    this.VisitValue(fc.IfTrue);
                    ifFalse = fc.IfFalse;
                }
                if (ifFalse != null)
                {
                    this.Write(", ");
                    this.VisitValue(ifFalse);
                }
                this.Write(")");

            }
            else
            {
                this.Write("(if( ");
                this.VisitValue(c.Test);
                this.Write(", 0, ");
                this.VisitValue(c.IfFalse);
                this.Write(", ");
                this.VisitValue(c.IfTrue);
                this.Write(")");
            }
            return c;
        }


    }
}
