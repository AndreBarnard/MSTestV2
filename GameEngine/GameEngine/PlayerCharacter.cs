using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace GameEngine
{
	public class PlayerCharacter : INotifyPropertyChanged
	{
		private int _health = 100;
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";

		public string NickName { get; set; }

		public int Health
		{
			get => _health;
			set
			{
				_health = value;
				//OnPropertyChanged();
			}
		}

		public bool IsNoob { get; set; }
		public List<string> Weapons { get; set; }

		public event EventHandler<EventArgs> PlayerSlept;

		public event PropertyChangedEventHandler PropertyChanged;

		public PlayerCharacter()
		{
			FirstName = GenerateRandomFirstName();
			IsNoob = true;

			CreateStartingWeapons();
		}

		private void CreateStartingWeapons()
		{
			
		}
		public void Sleep()
		{
			var healthIncrease = CalculateHealthIncrease();

			Health += healthIncrease;

			OnPlayerSlept(EventArgs.Empty);

		}

		private int CalculateHealthIncrease()
		{
			var rnd = new Random();

			return rnd.Next(1, 101);
		}

		protected virtual void OnPlayerSlept(EventArgs e)
		{
			PlayerSlept?.Invoke(this, e);
		}

		public void TakeDamage(int damage)
		{
			Health = Math.Max(1, Health -= damage);
		}

		private string GenerateRandomFirstName()
		{
			var possibleRandomSTartingNames = new[]
			{
				"Danieth",
				"Derick",
				"André"
			};

			return "Random Name";
		}		
	}
}
