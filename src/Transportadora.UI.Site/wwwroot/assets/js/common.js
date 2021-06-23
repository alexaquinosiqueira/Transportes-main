//adiciona mascara de cnpj
function MascaraCNPJ(cnpj) {
	if (mascaraInteiro(cnpj) == false) {
		event.returnValue = false;
	}
	return formataCampo(cnpj, '00.000.000/0000-00', event);
}

//adiciona mascara de cep
function MascaraCep(cep) {
	if (mascaraInteiro(cep) == false) {
		event.returnValue = false;
	}
	return formataCampo(cep, '00.000-000', event);
}

//adiciona mascara de data
function MascaraData(data) {
	if (mascaraInteiro(data) == false) {
		event.returnValue = false;
	}
	return formataCampo(data, '00/00/0000', event);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
	if (mascaraInteiro(tel) == false) {
		event.returnValue = false;
	}
	return formataCampo(tel, '(00) 0000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
	if (mascaraInteiro(cpf) == false) {
		event.returnValue = false;
	}
	return formataCampo(cpf, '000.000.000-00', event);
}

//valida telefone
function ValidaTelefone(tel) {
	exp = /\(\d{2}\)\ \d{4}\-\d{4}/
	if (!exp.test(tel.value))
		alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep) {
	exp = /\d{2}\.\d{3}\-\d{3}/
	if (!exp.test(cep.value))
		alert('Numero de Cep Invalido!');
}

//valida data
function ValidaData(data) {
	exp = /\d{2}\/\d{2}\/\d{4}/
	if (!exp.test(data.value))
		alert('Data Invalida!');
}

//valida o CPF digitado
function ValidarCPF(Objcpf) {
	var cpf = Objcpf.value;
	exp = /\.|\-/g
	cpf = cpf.toString().replace(exp, "");
	var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
	var soma1 = 0, soma2 = 0;
	var vlr = 11;

	for (i = 0; i < 9; i++) {
		soma1 += eval(cpf.charAt(i) * (vlr - 1));
		soma2 += eval(cpf.charAt(i) * vlr);
		vlr--;
	}
	soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
	soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

	var digitoGerado = (soma1 * 10) + soma2;
	if (digitoGerado != digitoDigitado)
		alert('CPF Invalido!');
}

//valida numero inteiro com mascara
function mascaraInteiro() {
	if (event.keyCode < 48 || event.keyCode > 57) {
		event.returnValue = false;
		return false;
	}
	return true;
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
	var cnpj = ObjCnpj.value;
	var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
	var dig1 = new Number;
	var dig2 = new Number;

	exp = /\.|\-|\//g
	cnpj = cnpj.toString().replace(exp, "");
	var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

	for (i = 0; i < valida.length; i++) {
		dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
		dig2 += cnpj.charAt(i) * valida[i];
	}
	dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
	dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

	if (((dig1 * 10) + dig2) != digito)
		alert('CNPJ Invalido!');

}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) {
	var boleanoMascara;

	var Digitato = evento.keyCode;
	exp = /\-|\.|\/|\(|\)| /g
	campoSoNumeros = campo.value.toString().replace(exp, "");

	var posicaoCampo = 0;
	var NovoValorCampo = "";
	var TamanhoMascara = campoSoNumeros.length;;

	if (Digitato != 8) { // backspace 
		for (i = 0; i <= TamanhoMascara; i++) {
			boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
				|| (Mascara.charAt(i) == "/"))
			boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
				|| (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
			if (boleanoMascara) {
				NovoValorCampo += Mascara.charAt(i);
				TamanhoMascara++;
			} else {
				NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
				posicaoCampo++;
			}
		}
		campo.value = NovoValorCampo;
		return true;
	} else {
		return true;
	}
}

//adiciona mascara ao RG
function MascaraRG(rg) {
	if ((rg) == false) {
		event.returnValue = false;
	}
	return formataCampo(rg, '00.000.000-0', event);
}

function mask(o, f) {
	setTimeout(function () {
		var v = mphone(o.value);
		if (v != o.value) {
			o.value = v;
		}
	}, 1);
}

function mphone(v) {
	var r = v.replace(/\D/g, "");
	r = r.replace(/^0/, "");
	if (r.length > 10) {
		r = r.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3");
	} else if (r.length > 5) {
		r = r.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1) $2-$3");
	} else if (r.length > 2) {
		r = r.replace(/^(\d\d)(\d{0,5})/, "($1) $2");
	} else {
		r = r.replace(/^(\d*)/, "($1");
	}
	return r;
}

function formatarLitragem(elemento) {
	var valor = elemento.value;

	if ($('#ExpenseType_Id option:selected').text() == "Diesel") {
		valor = valor + '';
		valor = parseInt(valor.replace(/[\D]+/g, ''));
		valor = valor + '';
		valor = valor.replace(/([0-9]{3})$/g, ",$1");

		if (valor.length > 6) {
			valor = valor.replace(/([0-9]{3}),([0-9]{3}$)/g, "$1,$2");
		}

		elemento.value = valor;
		if (valor == 'NaN') elemento.value = '';

	} else {
		valor = valor + '';
		valor = parseInt(valor.replace(/[\D]+/g, ''));
		valor = valor + '';
		valor = valor.replace(/([0-9]{2})$/g, ",$1");

		if (valor.length > 6) {
			valor = valor.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
		}

		elemento.value = valor;
		if (valor == 'NaN') elemento.value = '';
	}


}

function formatarMoeda(elemento) {
	var valor = elemento.value;

	valor = valor + '';
	valor = parseInt(valor.replace(/[\D]+/g, ''));
	valor = valor + '';
	valor = valor.replace(/([0-9]{2})$/g, ",$1");

	if (valor.length > 6) {
		valor = valor.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
	}

	elemento.value = valor;
	if (valor == 'NaN') elemento.value = '';
}

function converteMoedaFloat(valor) {

	if (valor === "") {
		valor = 0;
	} else {
		valor = valor.replace(".", "");
		valor = valor.replace(",", ".");
		valor = parseFloat(valor);
	}
	return valor;

}

function converteFloatMoeda( valor) {
	var valorFormatado = valor.toLocaleString('pt-BR', { minimumFractionDigits: 2 });

	return valorFormatado;
}



function somenteNumeros(num) {
	var er = /[^0-9.]/;
	er.lastIndex = 0;	
}

function getURLVar(key) {
	var value = [];

	var query = String(document.location).split('?');

	if (query[1]) {
		var part = query[1].split('&');

		for (i = 0; i < part.length; i++) {
			var data = part[i].split('=');

			if (data[0] && data[1]) {
				value[data[0]] = data[1];
			}
		}

		if (value[key]) {
			return value[key];
		} else {
			return '';
		}
	}
}

$(document).ready(function() {

	//Form Submit for IE Browser
	$('button[type=\'submit\']').on('click', function() {
		$("form[id*='form-']").submit();
	});

	// Highlight any found errors
	$('.text-danger').each(function() {
		var element = $(this).parent().parent();
		
		if (element.hasClass('form-group')) {
			element.addClass('has-error');
		}
	});
	
	if (localStorage.getItem('column-left') == 'active') {
		$('#button-menu i').replaceWith('<i class="fa fa-dedent fa-lg"></i>');
		
		$('#column-left').addClass('active');
		
		// Slide Down Menu
		$('#menu li.active').has('ul').children('ul').addClass('collapse in');
		$('#menu li').not('.active').has('ul').children('ul').addClass('collapse');
	} else {
		$('#button-menu i').replaceWith('<i class="fa fa-indent fa-lg"></i>');
		
		$('#menu li li.active').has('ul').children('ul').addClass('collapse in');
		$('#menu li li').not('.active').has('ul').children('ul').addClass('collapse');
	}

	// Menu button
	$('#button-menu').on('click', function() {
		// Checks if the left column is active or not.
		if ($('#column-left').hasClass('active')) {
			localStorage.setItem('column-left', '');

			$('#button-menu i').replaceWith('<i class="fa fa-indent fa-lg"></i>');

			$('#column-left').removeClass('active');

			$('#menu > li > ul').removeClass('in collapse');
			$('#menu > li > ul').removeAttr('style');
		} else {
			localStorage.setItem('column-left', 'active');

			$('#button-menu i').replaceWith('<i class="fa fa-dedent fa-lg"></i>');
			
			$('#column-left').addClass('active');

			// Add the slide down to open menu items
			$('#menu li.open').has('ul').children('ul').addClass('collapse in');
			$('#menu li').not('.open').has('ul').children('ul').addClass('collapse');
		}
	});

	// Menu
	$('#menu').find('li').has('ul').children('a').on('click', function() {
		if ($('#column-left').hasClass('active')) {
			$(this).parent('li').toggleClass('open').children('ul').collapse('toggle');
			$(this).parent('li').siblings().removeClass('open').children('ul.in').collapse('hide');
		} else if (!$(this).parent().parent().is('#menu')) {
			$(this).parent('li').toggleClass('open').children('ul').collapse('toggle');
			$(this).parent('li').siblings().removeClass('open').children('ul.in').collapse('hide');
		}
	});
	
	// Override summernotes image manager
	$('button[data-event=\'showImageDialog\']').attr('data-toggle', 'image').removeAttr('data-event');
	
	$(document).delegate('button[data-toggle=\'image\']', 'click', function() {
		$('#modal-image').remove();
		
		$(this).parents('.note-editor').find('.note-editable').focus();
				
		$.ajax({
			url: 'index.php?route=common/filemanager&token=' + getURLVar('token'),
			dataType: 'html',
			beforeSend: function() {
				$('#button-image i').replaceWith('<i class="fa fa-circle-o-notch fa-spin"></i>');
				$('#button-image').prop('disabled', true);
			},
			complete: function() {
				$('#button-image i').replaceWith('<i class="fa fa-upload"></i>');
				$('#button-image').prop('disabled', false);
			},
			success: function(html) {
				$('body').append('<div id="modal-image" class="modal">' + html + '</div>');
	
				$('#modal-image').modal('show');
			}
		});	
	});

	// Image Manager
	$(document).delegate('a[data-toggle=\'image\']', 'click', function(e) {
		e.preventDefault();
	
		var element = this;
	


		$(element).popover({
			html: true,
			placement: 'right',
			trigger: 'manual',
			content: function() {
				return '<button type="button" id="button-image" class="btn btn-primary"><i class="fa fa-pencil"></i></button> <button type="button" id="button-clear" class="btn btn-danger"><i class="fa fa-trash-o"></i></button>';
			}
		});
		
		$(element).popover('toggle');		
	
		$('#button-image').on('click', function() {
			$('#modal-image').remove();
	
			$.ajax({
				url: 'index.php?route=common/filemanager&token=' + getURLVar('token') + '&target=' + $(element).parent().find('input').attr('id') + '&thumb=' + $(element).attr('id'),
				dataType: 'html',
				beforeSend: function() {
					$('#button-image i').replaceWith('<i class="fa fa-circle-o-notch fa-spin"></i>');
					$('#button-image').prop('disabled', true);
				},
				complete: function() {
					$('#button-image i').replaceWith('<i class="fa fa-upload"></i>');
					$('#button-image').prop('disabled', false);
				},
				success: function(html) {
					$('body').append('<div id="modal-image" class="modal">' + html + '</div>');
	
					$('#modal-image').modal('show');
				}
			});
	
			$(element).popover('hide');
		});
	
		$('#button-clear').on('click', function() {
			$(element).find('img').attr('src', $(element).find('img').attr('data-placeholder'));
			
			$(element).parent().find('input').attr('value', '');
	
			$(element).popover('hide');
		});
	});
	
	// tooltips on hover
	$('[data-toggle=\'tooltip\']').tooltip({container: 'body', html: true});

	// Makes tooltips work on ajax generated content
	$(document).ajaxStop(function() {
		$('[data-toggle=\'tooltip\']').tooltip({container: 'body'});
	});	
});

// Autocomplete */
(function($) {
	$.fn.autocomplete = function(option) {
		return this.each(function() {
			this.timer = null;
			this.items = new Array();
	
			$.extend(this, option);
	
			$(this).attr('autocomplete', 'off');
			
			// Focus
			$(this).on('focus', function() {
				this.request();
			});
			
			// Blur
			$(this).on('blur', function() {
				setTimeout(function(object) {
					object.hide();
				}, 200, this);				
			});
			
			// Keydown
			$(this).on('keydown', function(event) {
				switch(event.keyCode) {
					case 27: // escape
						this.hide();
						break;
					default:
						this.request();
						break;
				}				
			});
			
			// Click
			this.click = function(event) {
				event.preventDefault();
	
				value = $(event.target).parent().attr('data-value');
	
				if (value && this.items[value]) {
					this.select(this.items[value]);
				}
			}
			
			// Show
			this.show = function() {
				var pos = $(this).position();
	
				$(this).siblings('ul.dropdown-menu').css({
					top: pos.top + $(this).outerHeight(),
					left: pos.left
				});
	
				$(this).siblings('ul.dropdown-menu').show();
			}
			
			// Hide
			this.hide = function() {
				$(this).siblings('ul.dropdown-menu').hide();
			}		
			
			// Request
			this.request = function() {
				clearTimeout(this.timer);
		
				this.timer = setTimeout(function(object) {
					object.source($(object).val(), $.proxy(object.response, object));
				}, 200, this);
			}
			
			// Response
			this.response = function(json) {
				html = '';
	
				if (json.length) {
					for (i = 0; i < json.length; i++) {
						this.items[json[i]['value']] = json[i];
					}
	
					for (i = 0; i < json.length; i++) {
						if (!json[i]['category']) {
							html += '<li data-value="' + json[i]['value'] + '"><a href="#">' + json[i]['label'] + '</a></li>';
						}
					}
	
					// Get all the ones with a categories
					var category = new Array();
	
					for (i = 0; i < json.length; i++) {
						if (json[i]['category']) {
							if (!category[json[i]['category']]) {
								category[json[i]['category']] = new Array();
								category[json[i]['category']]['name'] = json[i]['category'];
								category[json[i]['category']]['item'] = new Array();
							}
	
							category[json[i]['category']]['item'].push(json[i]);
						}
					}
	
					for (i in category) {
						html += '<li class="dropdown-header">' + category[i]['name'] + '</li>';
	
						for (j = 0; j < category[i]['item'].length; j++) {
							html += '<li data-value="' + category[i]['item'][j]['value'] + '"><a href="#">&nbsp;&nbsp;&nbsp;' + category[i]['item'][j]['label'] + '</a></li>';
						}
					}
				}
	
				if (html) {
					this.show();
				} else {
					this.hide();
				}
	
				$(this).siblings('ul.dropdown-menu').html(html);
			}
			
			$(this).after('<ul class="dropdown-menu"></ul>');
			$(this).siblings('ul.dropdown-menu').delegate('a', 'click', $.proxy(this.click, this));	
			
		});
	}
})(window.jQuery);

function empty(text){
   return '<option value="">-- select '+text+' --</option><option value=""></option>';
} 

function LoadHTML(url){
	var value;
	$('#loading').modal('show');
	$.ajax({
		url: base_url+''+url,
		dataType: 'html',
		async: false,
		success: function(data){
			setTimeout(function() {
				$('#loading').modal('hide');
			}, 1500);
			value = data;
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			//alert('Request Error !!, Please try again !!');
			$('#loading').modal('hide');
		},
		timeout: 5000
	});
	return value;
}
