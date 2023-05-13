/*!
 * Start Bootstrap - Creative v7.0.7 (https://startbootstrap.com/theme/creative)
 * Copyright 2013-2023 Start Bootstrap
 * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-creative/blob/master/LICENSE)
 */
//
// Scripts
//

window.addEventListener("DOMContentLoaded", event => {
	// Navbar shrink function
	var navbarShrink = function () {
		const navbarCollapsible = document.body.querySelector("#mainNav")
		if (!navbarCollapsible) {
			return
		}
		if (window.scrollY === 0) {
			navbarCollapsible.classList.remove("navbar-shrink")
		} else {
			navbarCollapsible.classList.add("navbar-shrink")
		}
	}

	// Shrink the navbar
	navbarShrink()

	// Shrink the navbar when page is scrolled
	document.addEventListener("scroll", navbarShrink)

	// Activate Bootstrap scrollspy on the main nav element
	const mainNav = document.body.querySelector("#mainNav")
	if (mainNav) {
		new bootstrap.ScrollSpy(document.body, {
			target: "#mainNav",
			rootMargin: "0px 0px -40%",
		})
	}

	// Collapse responsive navbar when toggler is visible
	const navbarToggler = document.body.querySelector(".navbar-toggler")
	const responsiveNavItems = [].slice.call(
		document.querySelectorAll("#navbarResponsive .nav-link")
	)
	responsiveNavItems.map(function (responsiveNavItem) {
		responsiveNavItem.addEventListener("click", () => {
			if (window.getComputedStyle(navbarToggler).display !== "none") {
				navbarToggler.click()
			}
		})
	})

	// Activate SimpleLightbox plugin for portfolio items
	new SimpleLightbox({
		elements: "#portfolio a.portfolio-box",
	})

	const submitButtonDisabled = () => {
		return Array.from(document.querySelectorAll(".required-input")).some(
			i => !i.value
		)
	}

	const submitButtonManage = () =>
		(document.querySelector("#submitButton").disabled = submitButtonDisabled())

	document
		.querySelectorAll("input")
		.forEach(i => i.addEventListener("change", event => submitButtonManage()))

	const textArea = document.querySelector("#message")

	textArea.addEventListener("change", event => submitButtonManage())
	textArea.addEventListener("input", event => submitButtonManage())
})

function formEnabled(enabled) {
	document.querySelector("#name").disabled = enabled
	document.querySelector("#phone").disabled = enabled
	document.querySelector("#email").disabled = enabled
	document.querySelector("#message").disabled = enabled
	document.querySelector("#submitButton").disabled = enabled
}

function onSubmitForm() {
	submit()
		.then(response => response.json())
		.then(response => {
			if (response == true) {
				document.querySelector("#submitSuccessMessage").className = ""
				document.querySelector("#submitErrorMessage").className = "d-none"
			} else {
				formEnabled(false)
				document.querySelector("#submitErrorMessage").className = ""
			}
		})
		.catch(error => {
			formEnabled(false)
			document.querySelector("#submitErrorMessage").className = ""
		})
	return false
}

function submit() {
	formEnabled(true)

	const body = {
		name: document.querySelector("#name").value,
		phone: document.querySelector("#phone").value,
		email: document.querySelector("#email").value,
		message: document.querySelector("#message").value,
	}

	return fetch("https://eiger-mailer.azurewebsites.net/Mail", {
		method: "POST",
		headers: {
			Accept: "application/json",
			"Content-Type": "application/json",
		},
		body: JSON.stringify(body),
	})
}
