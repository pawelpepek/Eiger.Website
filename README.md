# Eiger.Website

This is my friend's website [technikilinowe.pl](https://www.technikilinowe.pl/).

## Static website

Website hosted on Firebase.

Based on https://startbootstrap.com/theme/creative

Using:
- [SimpleLightbox](https://simplelightbox.com/),
- [Bootstrap](https://getbootstrap.com/),
- [Google Fonts](https://fonts.google.com/specimen/Merriweather+Sans/).

To make the page load faster, I added external css and JavaScript files to the source (styles.css contains Bootstrap styles).

I have created an SEO strategy for this website.

## WebAPI

I have created a WebAPi to send messages using the smtp protocol:
- hosted on the Azure cloud,
- with Azure CORS policy,
- with Azure ConnectionString configuration,
- with self-created DoS/DDoS attack mitigated service.
