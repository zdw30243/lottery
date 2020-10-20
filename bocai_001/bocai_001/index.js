//号码存入
_fun = {
	init: function() {
		var url = location.href;
		if (url != this.S('url')) {
			this.S('number', null);
			this.S('wz', null);
		};
		this.S('url', url);
		var arr = this.S('number');
		var wz = this.S('wz');
		if (arr != null) {
			for (var i = 0; i < arr.length; i++) {
				if (arr[i].length > 0) {
					for (var j = 0; j < $(".numContainer").eq(i).find('.selectMark').length; j++) {
						if ($(".numContainer").eq(i).find('.selectMark').eq(j).children().attr('name') == undefined) {
							if ($.inArray(parseInt($(".numContainer").eq(i).find('.selectMark').eq(j).children('.selectNumItem').text()),
									arr[i]) >= 0) {
								$(".numContainer").eq(i).find('.selectMark').eq(j).children('.selectNumItem').click();
							}
						} else {
							if ($.inArray($(".numContainer").eq(i).find('.selectMark').eq(j).children().attr('name'), arr[i]) >= 0) {
								var name = $(".numContainer").eq(i).find('.selectMark').eq(j).children().attr('name');
								$(".numContainer").eq(i).find('.selectItem[name=' + name + ']').click();
							}
						}
					}
				}
			}
		};
		if (wz != null) {
			if(wz.length==5){
				$('.all').text('清');
				$('.all').attr('val',1)
			};
			for (var i = 0; i < wz.length; i++) {
				$(".wzContainer").find('.selectWzItem[name=' + wz[i] + ']').click();
			}
		}
	},
	//选择号码储存
	NumberSelect: [],
	SelectArr: [],
	NumberStorage: function() {
		this.SelectArr = [];
		for (var a = 0; a < $(".numContainer").length; a++) {
			this.NumberSelect = [];
			var arr = [];
			for (var i = 0; i < $(".numContainer").eq(a).find('.selectNumItem.active,.selectItem.active').length; i++) {
				var num = $(".numContainer").eq(a).find('.selectNumItem.active,.selectItem.active').eq(i).attr('name');
				if (num == undefined) {
					this.NumberSelect.push(parseInt($(".numContainer").eq(a).find('.selectNumItem.active,.selectItem.active').eq(i).text()));
					arr.push(parseInt($(".numContainer").eq(a).find('.selectNumItem.active,.selectItem.active').eq(i).text()))
				} else {
					this.NumberSelect.push(num);
					arr.push(num);
				}
			};
			if (arr.length > 0) {
				this.SelectArr.push(arr);
			};
			$('.ts').eq(a).text('当前共' + this.NumberSelect.length + '球,至少' + this.NumberSelect.length * 1 + '元')
			$(".numberselect").eq(a).val(this.NumberSelect.join(','))
		};
		if (this.SelectArr.length > 0) {
			this.S('number', this.SelectArr);
		} else {
			this.S('number', null);
		};
		var count = 0;
		var y = true;
		typeof type != "undefined" ? y = true : y = false;
		if (!y) {
			return false;
		};
		switch (type) {
			case 'LmH2':
			case 'LmH3':
			case 'LmH4':
			case 'WxZx':
			case 'WxTx':
				count = this.Counts.Fs();
				break;
			case 'Rx2':
			case 'X2Lu':
				count = this.Counts.Zh(2);
				break;
			case 'Rx3':
			case 'X3Qu':
				count = this.Counts.Zh(3);
				break;
			case 'Rx4':
				count = this.Counts.Zh(4);
				break;
			case 'Rx5':
				count = this.Counts.Zh(5);
				break;
			default:
				break;
		}
		$('.zhushu').text('当前选号共' + count + '注')

	},
	//位置储存
	WzSelect: [],
	WzStorage: function() {
		this.WzSelect = [];
		var leng = $(".selectWzItem.active").length;
		if (leng <= 0){this.S('wz', null);}
		for (var i = 0; i < leng; i++) {
			this.WzSelect.push($(".selectWzItem.active").eq(i).attr('name'));
		};
		this.S('wz', this.WzSelect);
		var p = this.WzSelect.join(',');
		var url = $("form[name=form1]").attr('action');
		var xurl = url;
		if (leng > 0) xurl = this.changeURLArg(url, 'P', p);
		$("form[name=form1]").attr('action', xurl);
	},
	//修改url参数
	changeURLArg: function(url, arg, arg_val) {
		var pattern = arg + '=([^&]*)';
		var replaceText = arg + '=' + arg_val;
		if (url.match(pattern)) {
			var tmp = '/(' + arg + '=)([^&]*)/gi';
			tmp = url.replace(eval(tmp), replaceText);
			return tmp;
		} else {
			if (url.match('[\?]')) {
				return url + '&' + replaceText;
			} else {
				return url + '?' + replaceText;
			}
		}
	},
	//储存数据
	S: function(name, value) {
		var result;
		if (!isNaN(value) && value != null) {
			value = value.toString();
		};
		if (name) {
			if (!value && value !== null) {
				result = JSON.parse(localStorage.getItem(name))
			} else {
				if (value == null) {
					result = localStorage.removeItem(name)
				} else {
					stringifyValue = JSON.stringify(value);
					result = localStorage.setItem(name, stringifyValue)
				}
			}
		} else {
			return false;
		};
		return result;
	},
	//注数计算
	Counts: {
		//复试计算
		Fs: function() {
			var count = 1;
			for (var i = 0; i < _fun.SelectArr.length; i++) {
				count *= _fun.SelectArr[i].length;
			};
			return count;
		},
		//组合计算
		Zh: function(n) {
			var arr = [];
			var count = 0;
			for (var i = 0; i < Math.pow(2, _fun.SelectArr[0].length); i++) {
				var a = 0;
				var b = [];
				for (var j = 0; j < _fun.SelectArr[0].length; j++) {
					if (i >> j & 1) {
						a++;
						b.push(_fun.SelectArr[0][j]);
					}
				};
				if (a == n) {
					count++
				}
			}
			return count;
		},

	}
}
$(function() {
	//号码点击
	$(".selectNumItem,.selectItem").on('click', function() {
		if ($(this).is(".selectItem")) {
			$(this).toggleClass("active").parent().siblings().children().removeClass('active');
		} else {
			$(this).toggleClass("active");
			$(this).parent().parent().next().children().removeClass('active');
		}
		_fun.NumberStorage();
	});
	//位置点击
	$(".selectWzItem").on('click', function() {
		$(this).toggleClass("active");
		$(this).parent().parent().next().children().removeClass('active');
		_fun.WzStorage();
	});
	//玩法提示
	$(".tips").on('click', function() {
		$(".Explaininfo").toggleClass('hide');
	})

	//位置全
	$('.all').on('click',function(){
		var v=$(this).attr('val');
		if(v==0){
			$(this).parent().siblings().find('.selectWzItem').addClass('active');
			$(this).text('清');
			$(this).attr('val',1)
		}else{
			$(this).parent().siblings().find('.selectWzItem').removeClass('active');
			$(this).text('全');
			$(this).attr('val',0)
		};
		_fun.WzStorage();
	})


	//全
	$(".btnContainer").find('.btnItem:eq(0)').on('click', function() {
		$(this).parent().prev().children().children('.selectNumItem,.selectWzItem').addClass('active');
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//大
	$(".btnContainer").find('.btnItem:eq(1)').on('click', function() {
		var le = $(this).parent().prev().children().length;
		var z = Math.floor(le / 2);
		$(this).parent().prev().children().eq(z - 1).nextAll().children('.selectNumItem,.selectWzItem').addClass('active');
		$(this).parent().prev().children().eq(z).prevAll().children('.selectNumItem,.selectWzItem').removeClass('active');
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//小
	$(".btnContainer").find('.btnItem:eq(2)').on('click', function() {
		var le = $(this).parent().prev().children().length;
		var z = Math.floor(le / 2);
		$(this).parent().prev().children().eq(z - 1).nextAll().children('.selectNumItem,.selectWzItem').removeClass(
			'active');
		$(this).parent().prev().children().eq(z).prevAll().children('.selectNumItem,.selectWzItem').addClass('active');
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//单
	$(".btnContainer").find('.btnItem:eq(3)').on('click', function() {
		var le = $(this).parent().prev().children().length;
        var text, name;
      
		for (var i = 0; i < le; i++) {
            text = $(this).parent().prev().children().eq(i).children('.selectNumItem').text();
        
            name = $(this).parent().prev().children().eq(i).children('.selectWzItem').attr('name');
           // alert("我是一个警告框！2" + i);
       
			if (text % 2 == 0) {
				$(this).parent().prev().children().eq(i).children('.selectNumItem').removeClass('active');
			} else {
				$(this).parent().prev().children().eq(i).children('.selectNumItem').addClass('active');
			};
			if (name % 2 == 0) {
				$(this).parent().prev().children().eq(i).children('.selectWzItem').removeClass('active');
			} else {
				$(this).parent().prev().children().eq(i).children('.selectWzItem').addClass('active');
			};
		};
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//双
	$(".btnContainer").find('.btnItem:eq(4)').on('click', function() {
		var le = $(this).parent().prev().children().length;
		var text, name;
		for (var i = 0; i < le; i++) {
			text = $(this).parent().prev().children().eq(i).children('.selectNumItem').text();
			name = $(this).parent().prev().children().eq(i).children('.selectWzItem').attr('name');
			if (text % 2 == 0) {
				$(this).parent().prev().children().eq(i).children('.selectNumItem').addClass('active');
			} else {
				$(this).parent().prev().children().eq(i).children('.selectNumItem').removeClass('active');
			};
			if (name % 2 == 0) {
				$(this).parent().prev().children().eq(i).children('.selectWzItem').addClass('active');
			} else {
				$(this).parent().prev().children().eq(i).children('.selectWzItem').removeClass('active');
			}
		};
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//清
	$(".btnContainer").find('.btnItem:eq(5)').on('click', function() {
		$(this).parent().prev().children().children('.selectNumItem,.selectWzItem').removeClass('active');
		_fun.NumberStorage();
		_fun.WzStorage();
	});
	//
	$(".btnItem").on('click', function() {
		if ($(this).index() <= 4) {
			$(this).addClass('active').siblings().removeClass('active')
		} else {
			$(this).siblings().removeClass('active');
		}
	});
	//清(输入框号码清除)
	$(".clear").click(function() {
		$(this).prev().val('');
		if ($(this).prev().is(".numberselect")) {
			var index = $(this).parent().parent().index();
			$(".numContainer").eq(index).find('.selectNumItem,.selectItem.active').removeClass('active');
			$(".btnItem").removeClass("active")
			_fun.NumberStorage();
		};
		if ($(this).prev().is(".money")) {
			$('.quickItem').removeClass('quickItemactive')
		};
	})
	//快捷金额选择
	$(".quickItem").on('click', function() {
		$(this).addClass('quickItemactive').siblings().removeClass('quickItemactive')
		var money = $(this).attr('name');
		$(".money").val(money);
	});
	$(".submit").on('click', function() {
		_fun.S('number', null);
	})
	_fun.init();
})
