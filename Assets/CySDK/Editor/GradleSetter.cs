using System.Collections.Generic;
using UnityEditor;
using MiniGameSDK;
using System.IO;

namespace CySDK
{
    public class GradleSetter : IParamSettng
    {
        public void SetParam()
        {
            var gradle = GradleHelper.Open();
            var root = gradle.Root;
            var scp_dpn = root.FindNode("buildscript/dependencies");
            var n = scp_dpn.FindValue("classpath 'com.android.tools.build:gradle:3.4.0'");
            scp_dpn.Remove(n);
            scp_dpn.AddValue("classpath 'com.android.tools.build:gradle:3.4.3'");
            scp_dpn.AddValue("classpath 'com.google.gms:google-services:4.3.3'");
            var rep = root.FindNode("allprojects/repositories");
            var node = new GradleHelper.Node("maven");
            node.AddValue("url 'https://mvn.shalltry.com/repository/maven-public/'");
            rep.Add(node);
            node = new GradleHelper.Node("maven");
            node.AddValue("url 'https://mvn.shalltry.com/repository/game-releases/'");
            rep.Add(node);
            node = new GradleHelper.Node("maven");
            node.AddValue("url 'https://mvn.shalltry.com/repository/game-snapshots/'");
            rep.Add(node);
            node = new GradleHelper.Node("maven");
            node.AddValue("url 'http://localhost:8081/nexus/content/repositories/renrundong/'");
            rep.Add(node);
            root.InsertValueAfter("apply plugin: 'com.android.application'", "apply plugin: 'com.google.gms.google-services'");
            var dpn = root.FindNode("dependencies");
            dpn.AddValue("implementation 'com.transsion.game:unity:3.0.0.4'");
            dpn.AddValue("implementation 'com.transsion.game:ad:3.3.0.2'");
            dpn.AddValue("implementation 'com.transsion.game:analytics:3.3.0.1'");
            dpn.AddValue("implementation 'com.unity.androidapi:unityactivitygeter:1.0.0'");
            dpn.AddValue("implementation 'com.unity.androidapi:cyanalize:1.0.1'");
            var dcfg = root.FindNode("android/defaultConfig");
            dcfg.InsertValue(0, "multiDexEnabled true");
            gradle.Save();
            //AssetDatabase.Refresh();
        }
    }
}
