# Firmware Update V1.0 客户端 TN1900分支 说明

## 简介

本分支基于 stm32f407_gcore_rtt_prj tn19002分支，开发单逆测试台客户端应用


### 自动化测试协议
- 0x0D 0xE0 0xXX 0xXX 0xXX 0xXX 0xXX 0xXX 0xXX 0xXX 0xXX 0x0D
- data[2]
- 0x01 -> 负载效应0kW [输入电流] [输出电压] [输出电流] 
- 0x02 -> 负载效应0kW [输出频率] [输出电位差] [输出谐波]
- 0x03 -> 负载效应1.5kW [输入电流] [输出电压] [输出电流] 
- 0x04 -> 负载效应1.5kW [输出频率] [输出电位差] [输出谐波]
- 0x05 -> 负载效应3kW [输入电流] [输出电压] [输出电流] 
- 0x06 -> 负载效应3kW [输出频率] [输出电位差] [输出谐波]

- 0x11 -> 效率测试 [输入电压] [输入电流] [输出电压] [输出电流] 

- 0x21 -> 77V输入源效率测试  [输入电流] [输出电压] [输出电流] [输出电位差] 
- 0x22 -> 110V输入源效率测试  [输入电流] [输出电压] [输出电流] [输出电位差] 
- 0x23 -> 137.5V输入源效率测试  [输入电流] [输出电压] [输出电流] [输出电位差] 

- 0x31 -> 输入欠压保护(70V) [欠压点] [故障码] 
- 0x32 -> 输入过压保护(142V) [过压点] [故障码] 

- 0x41 -> 输出过载保护 [故障码] [输出电流]
 
## 上位机介绍

上位机采用C#开发，界面如下：


## 注意事项

- 暂无

## 联系人信息

- 维护人:Leon Kwok
- 联系方式：lz_kwok@163.com

