<template>
    <li class="nav-item">
        <a 
            v-bind:class = "(isActive)?'nav-link active':'nav-link'"
            aria-current="page" 
            href="#"
        >
            {{ navItemName }}
        </a>
    </li>
</template>

<script>
    export default {
        name: 'NavItem',
        props: {
            navItemName: String,
            isActive: Boolean
        }
    }
</script>