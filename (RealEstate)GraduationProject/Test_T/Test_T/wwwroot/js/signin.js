// Sign in & Sign up
const wrapper = document.querySelector(".wrapper");
const signinLink = document.querySelector(".signin-link");
const signupLink = document.querySelector(".signup-link");

signupLink.addEventListener("click", () => {
  wrapper.classList.add("active");
});

signinLink.addEventListener("click", () => {
  wrapper.classList.remove("active");
});

<script>
    var a;
    function pass() {
        if (a == 1) {
        document.getElementById("password").type = "password";
    document.getElementById("passicon").src = "/images/pass-hide.png";
    a = 0;
        }
    else {
        document.getElementById("password").type = "text";
    document.getElementById("passicon").src = "/images/pass-show.png";
    a = 1;
        }
    }
</script>