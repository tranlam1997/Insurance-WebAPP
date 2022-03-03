<template>
  <footer class="site-footer">
    <div class="flex flex-row gap-10 mx-20 mb-10 justify-center align-center">
      <div class="flex flex-col w-2/3">
        <h6>About us</h6>
        <p class="font-medium">
          Psawn Insurance is committed to providing a complete, balanced
          financial solution and building sound financial security to enable our
          customers to live and share their dreams with the next generation.
        </p>
      </div>

      <div class="text-center">
        <h6>Categories</h6>
        <ul class="footer-links font-medium">
          <li>
            <a href="http://scanfcode.com/category/front-end-development/"
              >Home Insurance</a
            >
          </li>
          <li>
            <a href="http://scanfcode.com/category/back-end-development/"
              >Life Insurance</a
            >
          </li>
          <li>
            <a href="http://scanfcode.com/category/java-programming-language/"
              >Medical Insurance</a
            >
          </li>
          <li>
            <a href="http://scanfcode.com/category/android/"
              >Vehicle Insurance</a
            >
          </li>
          <li>
            <a href="http://scanfcode.com/category/templates/"
              >Other Services</a
            >
          </li>
        </ul>
      </div>

      <div class="text-center">
        <h6>Quick Links</h6>
        <ul class="footer-links font-medium">
          <li><a href="http://scanfcode.com/about/">About Us</a></li>
          <li><a href="http://scanfcode.com/contact/">Contact Us</a></li>
          <li>
            <a href="http://scanfcode.com/contribute-at-scanfcode/"
              >Contribute</a
            >
          </li>
          <li>
            <a href="http://scanfcode.com/privacy-policy/">Privacy Policy</a>
          </li>
          <li><a href="http://scanfcode.com/sitemap/">Sitemap</a></li>
        </ul>
      </div>
    </div>
    <hr />

    <div class="mx-10 mt-4 font-medium">
      <div class="col-md-8 col-sm-6 col-xs-12">
        <p class="copyright-text">
          Copyright &copy; 2022 All Rights Reserved by
          <a href="#">Psawn Insurance</a>.
        </p>
      </div>

      <div class="col-md-4 col-sm-6 col-xs-12">
        <ul class="social-icons">
          <li>
            <a class="facebook" href="#"
              ><i class="fa-brands fa-facebook"></i
            ></a>
          </li>
          <li>
            <a class="twitter" href="#"><i class="fa-brands fa-twitter"></i></a>
          </li>
          <li>
            <a class="youtube" href="#"><i class="fa-brands fa-youtube"></i></a>
          </li>
          <li>
            <a class="linkedin" href="#"
              ><i class="fa-brands fa-linkedin"></i
            ></a>
          </li>
        </ul>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "Footer",
};
</script>

<style scoped lang="scss">
.site-footer {
  background-color: #161826;
  padding: 45px 0 20px;
  font-size: 15px;
  line-height: 24px;
  color: #737373;
}
.site-footer hr {
  border-top-color: #bbb;
  opacity: 0.5;
}
.site-footer hr.small {
  margin: 20px 0;
}
.site-footer h6 {
  color: #fff;
  font-size: 16px;
  text-transform: uppercase;
  margin-top: 5px;
  letter-spacing: 2px;
}
.site-footer a {
  color: #737373;
}
.site-footer a:hover {
  color: #3366cc;
  text-decoration: none;
}
.footer-links {
  padding-left: 0;
  list-style: none;
}
.footer-links li {
  display: block;
}
.footer-links a {
  color: #737373;
}
.footer-links a:active,
.footer-links a:focus,
.footer-links a:hover {
  color: #3366cc;
  text-decoration: none;
}
.footer-links.inline li {
  display: inline-block;
}
.site-footer .social-icons {
  text-align: right;
}
.site-footer .social-icons a {
  width: 40px;
  height: 40px;
  line-height: 40px;
  margin-left: 6px;
  margin-right: 0;
  border-radius: 100%;
  background-color: #33353d;
}
.copyright-text {
  margin: 0;
}
@media (max-width: 991px) {
  .site-footer [class^="col-"] {
    margin-bottom: 30px;
  }
}
@media (max-width: 767px) {
  .site-footer {
    padding-bottom: 0;
  }
  .site-footer .copyright-text,
  .site-footer .social-icons {
    text-align: center;
  }
}
.social-icons {
  padding-left: 0;
  margin-bottom: 0;
  list-style: none;
}
.social-icons li {
  display: inline-block;
  margin-bottom: 4px;
}
.social-icons li.title {
  margin-right: 15px;
  text-transform: uppercase;
  color: #96a2b2;
  font-weight: 700;
  font-size: 13px;
}
.social-icons a {
  background-color: #eceeef;
  color: #818a91;
  font-size: 16px;
  display: inline-block;
  line-height: 44px;
  width: 44px;
  height: 44px;
  text-align: center;
  margin-right: 8px;
  border-radius: 100%;
  -webkit-transition: all 0.2s linear;
  -o-transition: all 0.2s linear;
  transition: all 0.2s linear;
}
.social-icons a:active,
.social-icons a:focus,
.social-icons a:hover {
  color: #fff;
  background-color: #29aafe;
}
.social-icons.size-sm a {
  line-height: 34px;
  height: 34px;
  width: 34px;
  font-size: 14px;
}
.social-icons a.facebook:hover {
  background-color: #3b5998;
}
.social-icons a.twitter:hover {
  background-color: #00aced;
}
.social-icons a.linkedin:hover {
  background-color: #007bb6;
}
.social-icons a.dribbble:hover {
  background-color: #ea4c89;
}
@media (max-width: 767px) {
  .social-icons li.title {
    display: block;
    margin-right: 0;
    font-weight: 600;
  }
}
</style>
