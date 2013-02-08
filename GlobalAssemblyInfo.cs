using System; 
using System.Reflection; 
using System.Runtime.InteropServices; 
  
[assembly : ComVisible(false)] 
[assembly : CLSCompliant(true)] 
  
[assembly : AssemblyProduct("LightCore")] 
[assembly : AssemblyCompany("Peter Bucher")] 
[assembly : AssemblyCopyright("\x00a9 Peter Bucher. All rights reserved.")] 
  
[assembly : AssemblyVersion("1.5.1.0")] 
  
#if DEBUG 
[assembly : AssemblyConfiguration("Debug")] 
#else 
[assembly : AssemblyConfiguration("Release")] 
#endif