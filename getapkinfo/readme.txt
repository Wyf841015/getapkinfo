(4.19更新)全新通用无害解决无法从IPTV界面切换回第三方桌面的


编写人：一零一二
qq：64652178


部分华为悦盒进入IPTV界面后，无法返回当贝桌面，只能重启机顶盒。网上现有的方法均为通过更改系统文件来实现遥控返回，可能造成盒子无限重启无法开机或是IPTV界面“确定“按键无法使用的问题。新的脚本通过getevent及am命令，实现后台监控遥控器按键，不修改任何文件，运行稳定可靠。

本软件共分两部分

盒子脚本（exitiptv.sh&&exitiptvcfg.ini）【V1.0】
   2017.03.27 更新:1.更改脚本实现可自定义八组按键；
         2.与计算机端程序配合，添加将windows换行符\n\r转换为liunx换行符\r

计算机软件【V0.7】
    2017.04.02更新：1.调整通过adb shell dumpsys activity |find "mFocusedActivity"命令，直接与机顶盒交互自动获取启动信息（package/activity）;
                    2.编写新的ADB shell执行函数，能够更准确返回结果信息
				    3.优化文件推送，能够准确反应执行状态

    2017.03.27首版：1.实现计算机端根据apk文件，通过aapt命令自动获取启动信息（package/activity）;
                    2.通过adb shell getevent -c 1 /dev/input/event0命令，实现与机顶盒交互，获取遥控按键码；
                    3.自动生成与盒子脚本exitiptv.sh配合的exitiptvcfg.ini文件；
                    4.可一间将盒子脚本exitiptv.sh及配置文件exitiptvcfg.ini推送至机顶盒，并在/system/etc/install-recovery-2.sh自动添加“/system/etc/exitiptv.sh& ”，实现exitiptv.sh脚本开机启动





