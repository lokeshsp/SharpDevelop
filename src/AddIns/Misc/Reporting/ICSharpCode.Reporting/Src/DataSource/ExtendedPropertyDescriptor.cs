﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 20.05.2013
 * Time: 18:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace ICSharpCode.Reporting.DataSource
{
	/// <summary>
	/// Description of ExtendedPropertyDescriptor.
	/// </summary>
	internal class ExtendedPropertyDescriptor : PropertyDescriptor
	{
	
		Type componentType;
		Type propertyType;
		PropertyInfo prop;

		public ExtendedPropertyDescriptor (string name, Type componentType, Type propertyType)
			: base (name, null)
		{
			this.componentType = componentType;
			this.propertyType = propertyType;
		}


		public override object GetValue (object component)
		{
			var x = component.GetType();
			if (!componentType.IsAssignableFrom(component.GetType())){
				return null;
			}

			if (prop == null) {
				prop = componentType.GetProperty (Name);
			}
				
			object obj = prop.GetValue (component, null);
			if (obj != null) {
				if (obj is IList){
					PropertyTypeHash.Instance[componentType, Name] = DataCollection<object>.GetElementType((IList)obj, componentType, Name);
				}
			} 
			return obj;
		}

		
		public override void SetValue(object component,	object value)
		{
			if (IsReadOnly){
				return;
			}
			if (prop == null){
				prop = componentType.GetProperty (Name);
			}
			prop.SetValue (component, value, null);
		}

		public override void ResetValue(object component) 
		{
			return;
		}

		public override bool CanResetValue(object component) 
		{
			return false;
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get { return componentType; }
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override Type PropertyType
		{
			get { return propertyType; }
		}
	}
}
