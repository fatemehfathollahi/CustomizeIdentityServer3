﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.Common.Predicate
{
	internal class ParameterRebinder : ExpressionVisitor
	{
		private readonly Dictionary<ParameterExpression, ParameterExpression> map;

		private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
		{
			this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}

		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
		{
			return new ParameterRebinder(map).Visit(exp);
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			if (map.TryGetValue(p, out ParameterExpression replacement))
			{
				p = replacement;
			}

			return base.VisitParameter(p);
		}
	}
}