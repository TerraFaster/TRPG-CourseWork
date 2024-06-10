namespace CourseWork___TRPG
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ApplicationConfiguration.Initialize();

			Form form = null;

			// Parse command line arguments
			for (int i = 0; i < args.Length; i++)
			{
				// Create Map Editor Form
				if (args[i] == "-editor")
					form = new MapEditorForm();
			}

			// Create Main Form
			if (form == null)
				form = new MainForm();

			// Run the application
			Application.Run(form);
		}
	}
}