using GXPEngine;

namespace Lavos
{
	public abstract class Scene : GameObject
	{
		public abstract string Name { get; protected set; }

		public abstract void Start();

		public virtual void OnOffload() { }
	}
}