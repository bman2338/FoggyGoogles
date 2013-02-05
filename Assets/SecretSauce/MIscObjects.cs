using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SecretSauce
{
	public class MiscObjects
	{
		
		public MiscObjects ()
		{
		}
		/** These reset every run. */
		private static string LogLocation = 
			@"D:/Users/Chris/Documents/FreeSpace/Assets/Logs/";
		private static HashSet<string> scrapLogs = new HashSet<string>();
		public static void ScrapLog(string fn,string message){
			var file_name = LogLocation + fn + ".txt";
			if(!scrapLogs.Contains(fn)){
				File.WriteAllText(file_name, String.Empty);
			}
			
		    StreamWriter writer= File.AppendText(file_name);
	        try   
	        {    
				writer.WriteLine(message);
				scrapLogs.Add(fn);
	        }      
	        catch (Exception e)
	        {
				Debug.Log("Unable to write to file:" + fn);
				Debug.Log (e.Message);
			}	
	        finally
	        {
	            writer.Close();
	        }
			
		}
		
		public static void LogDebug(string m){
			if(Config.IS_DEBUG){
				Debug.Log (m);	
			}
		}
	}
}

