using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatConverter
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public String InputFile { get; set; } = "";
		public String OutputPath { get; set; } = "";

		public CommandAction SelectFileCommand { get; set; }
		public CommandAction SelectPathCommand { get; set; }
		public CommandAction ExecuteCommand { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public ViewModel()
		{
			SelectFileCommand = new CommandAction(SelectFile);
			SelectPathCommand = new CommandAction(SelectPath);
			ExecuteCommand = new CommandAction(Execute);
		}

		private void SelectFile(object? obj)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			InputFile = dlg.FileName;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputFile)));
		}

		private void SelectPath(object? obj)
		{
			var dlg = new OpenFolderDialog();
			if (dlg.ShowDialog() == false) return;

			OutputPath = dlg.FolderName;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OutputPath)));
		}

		private void Execute(object? obj)
		{
			if (!System.IO.File.Exists(InputFile)) return;
			if(!System.IO.Directory.Exists(OutputPath))
			{
				System.IO.Directory.CreateDirectory(OutputPath);
			}
			if (!System.IO.Directory.Exists(OutputPath)) return;

			var lines = System.IO.File.ReadAllLines(InputFile);
			var path = "";
			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				if (line.Length < 2) continue;

				if (line.IndexOf('[') >= 0)
				{
					CreateCheat(path, sb);
					sb.Clear();

					// exclusion []
					path = System.IO.Path.Combine(OutputPath, line.Substring(1, line.Length - 2));
					continue;
				}

				sb.AppendLine(line);
			}

			CreateCheat(path, sb);
		}

		private void CreateCheat(String path, StringBuilder sb)
		{
			// empty do nothing
			if (String.IsNullOrEmpty(path)) return;
			if (sb.Length == 0) return;

			try
			{
				System.IO.Directory.CreateDirectory(path);
				path = System.IO.Path.Combine(path, "cheats");
				System.IO.Directory.CreateDirectory(path);

				var filename = System.IO.Path.Combine(path, System.IO.Path.GetFileName(InputFile));
				System.IO.File.WriteAllText(filename, sb.ToString());
			}
			catch { }
		}
	}
}
