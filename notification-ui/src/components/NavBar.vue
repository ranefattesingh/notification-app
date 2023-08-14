<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">NotificationApp</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <nav-item navItemName="Item1" v-bind:is-active="true"></nav-item>
                    <nav-item navItemName="Item2" v-bind:is-active="false"></nav-item>
                    <nav-item navItemName="Item3" v-bind:is-active="false"></nav-item>
                    <nav-item navItemName="Item4" v-bind:is-active="false"></nav-item>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
    import NavItem from './NavItem.vue'

    export default {
        name: 'NavBar',
        components: {
            NavItem,
        }
    }
</script>