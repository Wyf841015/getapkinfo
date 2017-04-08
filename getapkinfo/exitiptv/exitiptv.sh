#!/system/bin/sh
#--------------------------------------------
# 作者：一零一二
# 版本：1.1
# 功能：实现华为悦盒IPTV遥控器"本地"键退出应用程序
# 更新：不再需要sm管理器及电视上操作，全部在本地脚本完成
# 2017.4.7更新：更换转换windows和Linux系统的换行符的方式，解决多次运行导致配置文件出错问题
# 2017.4.7更新：添加脚本运行日志，便于问题分析
# 2017.4.7更新：添加脚本运行状态检测，保证只有一个脚本常态运行
# 联系方式：qq：64652178
#--------------------------------------------


#挂在/system目录为可读写
mount -o remount,rw /system

#确保只有一个脚本实例运行
#判断脚本是否运行，如果已经运行，则退出
PN=`busybox basename $0`
echo $PN
Pnum=`busybox pgrep -f $PN|busybox wc -l`

if [ $Pnum -ge 3 ]
then
        echo "exitiptv.sh running"
        exit 3
fi



# __readINI [配置文件路径+名称] [节点名] [键值]
function __readINI() {
 INIFILE=$1; SECTION=$2; ITEM=$3
 _readIni=`busybox awk -F '=' '/\['$SECTION'\]/{a=1}a==1&&$1~/'$ITEM'/{print $2;exit}' $INIFILE`

echo ${_readIni}
}

# 
# 记录脚本启动日志
echo `date`":exitiptv.sh  running">>/system/etc/exitiptv.log

# 
# 转换windows和Linux系统的换行符
# busybox sed -i 's/.$//'  /system/etc/exitiptvcfg.ini
cat /system/etc/exitiptvcfg.ini|busybox tr -d "\r">/system/etc/exitiptvcfg1.ini


# 
# 获取配置的第一个遥控码
_rm1=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm1 ) ) 
#替换遥控码中的-为空格
rmcode1=${_rm1//-/ }
echo $rmcode1

# 
# 获取配置的第二个遥控码
_rm2=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm2 ) ) 
#替换遥控码中的-为空格
rmcode2=${_rm2//-/ }
echo $rmcode2

# 
# 获取配置的第三遥控码
_rm3=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm3 ) ) 
#替换遥控码中的-为空格
rmcode3=${_rm3//-/ }
echo $rmcode3

# 
# 获取配置的第四遥控码
_rm4=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm4 ) ) 
#替换遥控码中的-为空格
rmcode4=${_rm4//-/ }
echo $rmcode4
# 
# 获取配置的第五个遥控码
_rm5=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm5 ) ) 
#替换遥控码中的-为空格
rmcode5=${_rm5//-/ }
echo $rmcode5
# 
# 获取配置的第六个遥控码
_rm6=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm6 ) ) 
#替换遥控码中的-为空格
rmcode6=${_rm6//-/ }
echo $rmcode6
# 
# 获取配置的第七个遥控码
_rm7=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm7 ) ) 
#替换遥控码中的-为空格
rmcode7=${_rm7//-/ }
echo $rmcode7
# 
# 获取配置的第八个遥控码
_rm8=( $( __readINI /system/etc/exitiptvcfg1.ini  rm rm8 ) ) 
#替换遥控码中的-为空格
rmcode8=${_rm8//-/ }
echo $rmcode8

echo `date`":exitiptv.sh RMCode get success!" >>/system/etc/exitiptv.log

# 
# 获取配置的第一个切换APP
app1=( $( __readINI /system/etc/exitiptvcfg1.ini  app app1 ) ) 
echo $app1

#
# 获取配置的第二个切换APP
app2=( $( __readINI /system/etc/exitiptvcfg1.ini  app app2 ) ) 
echo $app2

#
# 获取配置的第三个切换APP
app3=( $( __readINI /system/etc/exitiptvcfg1.ini  app app3 ) ) 
echo $app3

#
# 获取配置的第四个切换APP
app4=( $( __readINI /system/etc/exitiptvcfg1.ini  app app4 ) ) 
echo $app4

#
# 获取配置的第五个切换APP
app5=( $( __readINI /system/etc/exitiptvcfg1.ini  app app5 ) ) 
echo $app5

#
# 获取配置的第六个切换APP
app6=( $( __readINI /system/etc/exitiptvcfg1.ini  app app6 ) ) 
echo $app6

#
# 获取配置的第七个切换APP
app7=( $( __readINI /system/etc/exitiptvcfg1.ini  app app7 ) ) 
echo $app7

#
# 获取配置的第八个切换APP
app8=( $( __readINI /system/etc/exitiptvcfg1.ini  app app8 ) ) 
echo $app8

echo `date`":exitiptv.sh APPInfo get success!" >>/system/etc/exitiptv.log

#
#循环监测开始开始
while [ 1 ]
do
	#获取当前遥控器按键
             inputcode=`getevent -c 1 /dev/input/event0`	
              echo  "inputcode:"$inputcode

	#检测第一个遥控码是否符合
	if [ "$inputcode" = "$rmcode1" ] 
               then
                 echo "rmcode1:"$rmcode1
	   am start -n "$app1"
              fi

    #检测第二个遥控码是否符合
	if [ "$inputcode" = "$rmcode2" ] ;
                 then
                   echo "rmcode2:"$rmcode2
	     am start -n "$app2"
              fi
	#检测第三个遥控码是否符合
	if [ "$inputcode" = "$rmcode3" ] ;
                 then
                   echo "rmcode3:"$rmcode3
	     am start -n "$app3"
              fi
    #检测第四个遥控码是否符合
	if [ "$inputcode" = "$rmcode4" ] ;
                 then
                   echo "rmcode4:"$rmcode4
	     am start -n "$app4"
              fi
	#检测第五遥控码是否符合
	if [ "$inputcode" = "$rmcode5" ] ;
                 then
                   echo "rmcode5:"$rmcode5
	     am start -n "$app5"
              fi
	#检测第六个遥控码是否符合
	if [ "$inputcode" = "$rmcode6" ] ;
                 then
                   echo "rmcode6:"$rmcode6
	     am start -n "$app6"
              fi
	#检测第七个遥控码是否符合
	if [ "$inputcode" = "$rmcode7" ] ;
                 then
                   echo "rmcode7:"$rmcode7
	     am start -n "$app7"
              fi
    #检测第八个遥控码是否符合
	if [ "$inputcode" = "$rmcode8" ] ;
                 then
                   echo "rmcode8:"$rmcode8
	     am start -n "$app8"
              fi
done
echo `date`":exitiptv.sh exit" >>/system/etc/exitiptv.log
#程序结束
