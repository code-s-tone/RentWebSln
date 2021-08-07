let options = {
    useEasing: true, 		// 使用缓和
    useGrouping: true, 		// 使用分组(是否显示千位分隔符,一般为 true)
    separator: ',', 		// 分隔器(千位分隔符,默认为',')
    decimal: '.', 			// 十进制(小数点符号,默认为 '.')
    prefix: '',				// 字首(数字的前缀,根据需要可设为 $,¥,￥ 等)
    suffix: '' 				// 后缀(数字的后缀 ,根据需要可设为 元,个,美元 等) 
};

        // CountUp(参数一, 参数二, 参数三, 参数四, 参数五, 参数六)
        // 参数一: 数字所在容器
        // 参数二: 数字开始增长前的默认值(起始值),一般从 0 开始增长
        // 参数三: 数字增长后的最终值,该值一般通过异步请求获取
        // 参数四: 数字小数点后保留的位数
        // 参数五: 数字增长特效的时间,此处为3秒
        // 参数六: 其他配置项
        // 注: 参数六也可不加,其配置项则为默认值
new CountUp("num-dynamic", 0, 60000, 0, 2).start();

setInterval(function(){ 
    new CountUp("num-dynamic", 0, 60000, 0, 3).start(); 
}, 6000);

// new CountUp("num2", 0, 1380, 2, 3, options).start();
// new CountUp("num3", 0, 1380, 4, 3, options).start();