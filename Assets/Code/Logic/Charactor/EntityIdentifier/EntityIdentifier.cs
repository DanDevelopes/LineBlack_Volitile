using Godot;
using Logic.Character.EntityLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Logic.Charactor.EntityIdentifier
{
	public static class EntityIdentifier
	{
		private static readonly object idLock = new object();
		private static List<EntityModel> enitityModels = new List<EntityModel>();
		public static int GetNewID()
		{
			lock(idLock)
			{
				EntityModel entityModel = new(); 
				if (enitityModels.Any())
				{
					int id = 1 + enitityModels.Max(x => x.id);
					entityModel.id = id;
					enitityModels.Add(entityModel);
					return entityModel.id;
				}
				else
				{
					entityModel.id = 1;
					enitityModels.Add(entityModel);
					return entityModel.id;
				}
			}

		}
		public static void SetLocation(Godot.Vector2 location, int id)
		{
			lock(idLock)
			{
				enitityModels.Where(x => x.id == id).Select(x => x).FirstOrDefault().location = location;
			}
		}
		public static void SetUniqueIdentifier(string uniqueID, int id)
		{
			lock(idLock)
			{
				Validation validation = IsUniqueIdValid(uniqueID, id);
				if(validation.isValid)
				{
					enitityModels.Where(x => x.id == id).FirstOrDefault().uniqueID = uniqueID;
				}
				else
				{
					Task.Run(async () => await Logger.LogException(validation.exception));
				}
			}
		}

		private static Validation IsUniqueIdValid(string uniqueID, int id)
		{
			Validation validation = new();
			lock (idLock)
			{
				try 
				{

					if(enitityModels.Exists(x => x.uniqueID == uniqueID && x.id != id))
					{
						throw new ArgumentException("UniqueID already exists int other entity");
					}
					if(enitityModels.Exists(x => x.uniqueID == uniqueID && x.id == id))
					{
						throw new ArgumentException("UniqueID already set to coresponding enitity");
					}
					if(!enitityModels.Exists(x => x.id == id))
					{
						throw new ArgumentException("Enitity is does not exist");
					}
					validation.isValid = true;
					return validation;
				}
				catch(Exception ex)
				{
					validation.isValid = false;
					validation.exception = ex;
					return validation;
				}
			}
		}
		public static Godot.Vector2 GetLocation(int id)
		{
			lock (idLock)
			{
				return enitityModels.Where(x => x.id == id).FirstOrDefault().location;
			}
		}
	}
	public class Validation
	{
		public bool isValid{ get; set; }
		public Exception exception{ get; set; }
	}
}