using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace Chocolatey.AutoUpdater.Configuration
{
    public abstract class AppSettingBase<T>
    {
        protected double ParseDouble<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            return double.Parse(AppSetting(propertySelector));
        }

        protected int ParseInt<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            return int.Parse(AppSetting(propertySelector));
        }

        protected IReadOnlyCollection<string> ParseCollection<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            var activedTasks = this.AppSetting(propertySelector);
            return activedTasks.Split(',').ToList();
        }

        protected string AppSetting<TValue>(Expression<Func<T, TValue>> propertySelector, string additionalNamePart = "")
        {
            MemberExpression memberExpression = GetMemberExpression(propertySelector);
            if (memberExpression == null)
                return (string)null;
            return this.AppSetting(this.GetName<TValue>(additionalNamePart, memberExpression));
        }

        protected string AppSetting(string configKey)
        {
            string appSetting = ConfigurationManager.AppSettings[configKey];
            if (!string.IsNullOrEmpty(appSetting))
                return appSetting;
            throw new Exception(configKey + " is not configured");
        }

        private string GetName<TValue>(string additionalNamePart, MemberExpression memberExpression)
        {
            return memberExpression.Member.Name + additionalNamePart;
        }

        private MemberExpression GetMemberExpression<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            return propertySelector.Body as MemberExpression;
        }
    }
}
