<template>
    <div class="container">
        <form>
            <h3 class="h2 text-center">Forgot Password</h3>
            <div class="form-group">
                <label>Email address</label>
                <input type="email" class="form-control form-control-lg" />
            </div>
            <div class="d-grid gap-2">
            <button type="submit" class="btn btn-dark btn-lg mt-3">Reset password</button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'ForgotPassword',
  data() {
    return {}
  },
});
</script>