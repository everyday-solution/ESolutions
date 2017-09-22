using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ESolutions.Collections.Generic
{
	public class ListWithEvents<T> : IList<T>
	{
		//Classes
		#region AddEventArgs
		public class AddEventArgs : EventArgs
		{
			#region item
			private T item;
			#endregion

			#region Item
			/// <summary>
			/// Gets the added item.
			/// </summary>
			/// <value>The item.</value>
			public T Item
			{
				get
				{
					return this.item;
				}
			}
			#endregion

			#region AddEventArgs
			/// <summary>
			/// Initializes a new instance of the <see cref="AfterAddEventArgs"/> class.
			/// </summary>
			/// <param name="item">The item.</param>
			public AddEventArgs(T item)
			{
				this.item = item;
			}
			#endregion
		}
		#endregion

		#region RemoveEventArgs
		public class RemoveEventArgs : EventArgs
		{
			#region item
			private T item;
			#endregion

			#region Item
			/// <summary>
			/// Gets the added item.
			/// </summary>
			/// <value>The item.</value>
			public T Item
			{
				get
				{
					return this.item;
				}
			}
			#endregion

			#region RemoveEventArgs
			/// <summary>
			/// Initializes a new instance of the <see cref="AfterAddEventArgs"/> class.
			/// </summary>
			/// <param name="item">The item.</param>
			public RemoveEventArgs(T item)
			{
				this.item = item;
			}
			#endregion
		}
		#endregion

		//Delegates
		#region AfterAddEventHandler
		public delegate void AfterAddEventHandler(object sender, AddEventArgs e);
		#endregion

		#region AfterRemoveEventHandler
		public delegate void AfterRemoveEventHandler(object sender, RemoveEventArgs e);
		#endregion

		//Fields
		#region items
		/// <summary>
		/// Internal storage for the items in the list
		/// </summary>
		List<T> items = new List<T>();
		#endregion

		//Events
		#region AfterAdd
		/// <summary>
		/// Occurs each time after a new item is added to the list.
		/// </summary>
		public event AfterAddEventHandler AfterAdd;
		#endregion

		#region AfterRemove
		/// <summary>
		/// Occurs each time an item is removed from the list.
		/// </summary>
		public event AfterRemoveEventHandler AfterRemove;
		#endregion

		// Properties
		#region this
		/// <summary>
		/// Gets or sets the <see cref="T"/> at the specified indexes.
		/// </summary>
		/// <value></value>
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				this.items[index] = value;
			}
		}
		#endregion

		//Methods
		#region IndexOf
		/// <summary>
		/// Bestimmt den Index eines bestimmten Elements in der <see cref="T:System.Collections.Generic.IList`1"></see>.
		/// </summary>
		/// <param name="item">Das im <see cref="T:System.Collections.Generic.IList`1"></see> zu suchende Objekt.</param>
		/// <returns>
		/// Der Index von item, wenn das Element in der Liste gefunden wird, andernfalls -1.
		/// </returns>
		public Int32 IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}
		#endregion

		#region Insert
		/// <summary>
		/// Fügt am angegebenen Index ein Element in die <see cref="T:System.Collections.Generic.IList`1"></see> ein.
		/// </summary>
		/// <param name="indexes">Der nullbasierte Index, an dem item eingefügt werden soll.</param>
		/// <param name="item">Das in die <see cref="T:System.Collections.Generic.IList`1"></see> einzufügende Objekt.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">indexes ist kein gültiger Index in der <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
		/// <exception cref="T:System.NotSupportedException">Die <see cref="T:System.Collections.Generic.IList`1"></see> ist schreibgeschützt.</exception>
		public void Insert(Int32 index, T item)
		{
			this.items.Insert(index, item);
			this.OnAfterAdd(item);
		}
		#endregion

		#region RemoveAt
		/// <summary>
		/// Entfernt das <see cref="T:System.Collections.Generic.IList`1"></see>-Element am angegebenen Index.
		/// </summary>
		/// <param name="indexes">Der nullbasierte Index des zu entfernenden Elements.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">indexes ist kein gültiger Index in der <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
		/// <exception cref="T:System.NotSupportedException">Die <see cref="T:System.Collections.Generic.IList`1"></see> ist schreibgeschützt.</exception>
		public void RemoveAt(Int32 index)
		{
			T item = this.items[index];
			this.items.RemoveAt(index);
			this.OnAfterRemove(item);
		}
		#endregion

		#region Add
		/// <summary>
		/// Fügt der <see cref="T:System.Collections.Generic.ICollection`1"></see> ein Element hinzu.
		/// </summary>
		/// <param name="item">Das Objekt, das <see cref="T:System.Collections.Generic.ICollection`1"></see> hinzugefügt werden soll.</param>
		/// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"></see> ist schreibgeschützt.</exception>
		public void Add(T item)
		{
			this.items.Add(item);
			this.OnAfterAdd(item);
		}
		#endregion

		#region AddRange
		/// <summary>
		/// Adds the range of items to the list.
		/// </summary>
		/// <param name="collection">The collection of items which shall be added.</param>
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T current in collection)
			{
				this.Add(current);
			}
		}
		#endregion

		#region Clear
		/// <summary>
		/// Entfernt alle Elemente aus <see cref="T:System.Collections.Generic.ICollection`1"></see>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"></see> ist schreibgeschützt. </exception>
		public void Clear()
		{
			this.items.Clear();
		}
		#endregion

		#region Contains
		/// <summary>
		/// Bestimmt, ob <see cref="T:System.Collections.Generic.ICollection`1"></see> einen bestimmten Wert enthält.
		/// </summary>
		/// <param name="item">Das im <see cref="T:System.Collections.Generic.ICollection`1"></see> zu suchende Objekt.</param>
		/// <returns>
		/// true, wenn sich item in <see cref="T:System.Collections.Generic.ICollection`1"></see> befindet, andernfalls false.
		/// </returns>
		public Boolean Contains(T item)
		{
			return this.items.Contains(item);
		}
		#endregion

		#region CopyTo
		/// <summary>
		/// Kopiert die Elemente von <see cref="T:System.Collections.Generic.ICollection`1"></see> in ein <see cref="T:System.Array"></see>, beginnend bei einem bestimmten <see cref="T:System.Array"></see>-Index.
		/// </summary>
		/// <param name="array">Das eindimensionale <see cref="T:System.Array"></see>, das das Ziel der aus <see cref="T:System.Collections.Generic.ICollection`1"></see> kopierten Elemente ist. Für <see cref="T:System.Array"></see> muss eine nullbasierte Indizierung verwendet werden.</param>
		/// <param name="arrayIndex">Der nullbasierte Index in array, ab dem kopiert wird.</param>
		/// <exception cref="T:System.ArgumentNullException">array ist null.</exception>
		/// <exception cref="T:System.ArgumentException">array ist mehrdimensional.– oder –arrayIndex ist größer oder gleich der Länge von array.– oder –Die Anzahl der aus der Quell-<see cref="T:System.Collections.Generic.ICollection`1"></see> zu kopierenden Elemente ist größer als der verfügbare Platz von arrayIndex bis zum Ende des Ziel-array.– oder –Typ T kann nicht automatisch in den Typ des Ziel-array umgewandelt werden.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">arrayIndex ist kleiner als 0 (null).</exception>
		public void CopyTo(T[] array, Int32 arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}
		#endregion

		#region Count
		/// <summary>
		/// Ruft die Anzahl der Elemente ab, die in <see cref="T:System.Collections.Generic.ICollection`1"></see> enthalten sind.
		/// </summary>
		/// <value></value>
		/// <returns>Die Anzahl der Elemente, die in <see cref="T:System.Collections.Generic.ICollection`1"></see> enthalten sind.</returns>
		public Int32 Count
		{
			get
			{
				return this.items.Count;
			}
		}
		#endregion

		#region IsReadOnly
		/// <summary>
		/// Ruft einen Wert ab, der angibt, ob <see cref="T:System.Collections.Generic.ICollection`1"></see> schreibgeschützt ist.
		/// </summary>
		/// <value></value>
		/// <returns>true, wenn <see cref="T:System.Collections.Generic.ICollection`1"></see> schreibgeschützt ist, andernfalls false.</returns>
		Boolean ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}
		#endregion

		#region Remove
		/// <summary>
		/// Entfernt das erste Vorkommen eines bestimmten Objekts aus <see cref="T:System.Collections.Generic.ICollection`1"></see>.
		/// </summary>
		/// <param name="item">Das aus dem <see cref="T:System.Collections.Generic.ICollection`1"></see> zu entfernende Objekt.</param>
		/// <returns>
		/// true, wenn item erfolgreich aus <see cref="T:System.Collections.Generic.ICollection`1"></see> gelöscht wurde, andernfalls false. Diese Methode gibt auch dann false zurück, wenn item nicht in der ursprünglichen <see cref="T:System.Collections.Generic.ICollection`1"></see> gefunden wurde.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"></see> ist schreibgeschützt.</exception>
		public Boolean Remove(T item)
		{
			Boolean result = this.items.Remove(item);
			this.OnAfterRemove(item);
			return result;
		}
		#endregion

		#region GetEnumerator
		/// <summary>
		/// Gibt einen Enumerator zurück, der die Auflistung durchläuft.
		/// </summary>
		/// <returns>
		/// Ein <see cref="T:System.Collections.Generic.IEnumerator`1"></see>, der zum Durchlaufen der Auflistung verwendet werden kann.
		/// </returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}
		#endregion

		#region GetEnumerator
		/// <summary>
		/// Gibt einen Enumerator zurück, der eine Auflistung durchläuft.
		/// </summary>
		/// <returns>
		/// Ein <see cref="T:System.Collections.IEnumerator"></see>-Objekt, das zum Durchlaufen der Auflistung verwendet werden kann.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}
		#endregion

		#region OnAfterAdd
		/// <summary>
		/// Fires the AfterAdd event.
		/// </summary>
		/// <param name="item">The item.</param>
		protected void OnAfterAdd(T item)
		{
			this.AfterAdd?.Invoke(this, new AddEventArgs(item));
		}
		#endregion

		#region OnAfterRemoveEvent
		/// <summary>
		/// Fires the AfterRemove event
		/// </summary>
		/// <param name="item">The removed item</param>
		protected void OnAfterRemove(T item)
		{
			this.AfterRemove?.Invoke(this, new RemoveEventArgs(item));
		}
		#endregion
	}
}
