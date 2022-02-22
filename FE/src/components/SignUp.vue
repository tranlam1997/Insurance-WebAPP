<template>
    <div class="container">
        <form>
            <h3 class="fw-bolder h2 text-center pb-4">Sign up to Psawn Insurance</h3>
            <div class="form-group">
                <label>Full Name</label>
                <input type="text" class="form-control form-control-lg"/>
            </div>
            <div class="form-group">
                <label>Email address</label>
                <input type="email" class="form-control form-control-lg" />
            </div>
            <div class="form-group">
                <label>Password</label>
                <input type="password" class="form-control form-control-lg" />
            </div>
            <div class="d-grid gap-2">
                <button type="submit" class="btn btn-dark btn-lg mt-3">Sign up</button>
            </div>
            <p class="forgot-password mt-3">
                Already registered 
                <router-link :to="{name: 'login'}">Sign in?</router-link>
            </p>
        </form>
    </div>
</template>
<script>
    export default {
        data() {
            return {}
        }
    }
</script>