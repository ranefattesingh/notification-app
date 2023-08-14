<template>
    <NavBar />
    <Notification />
</template>

<script>
    import NavBar from './NavBar.vue'
    import Notification from './Notification.vue'

    export default {
        name: 'Header',
        components: {
            NavBar,
            Notification
        }
    }
</script>