<template>
    <div>
        <div class="notification-item">
            <a type="button" class="icon-button" @click="isOpen=!isOpen">
                <span class="material-icons">notifications</span>
                <span class="icon-button__badge">{{unreadNotificationCount()}}</span>
            </a>
        </div>
        <notification-list v-bind:notifications="notifications" v-bind:isOpen="isOpen"></notification-list>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue'
    import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr'
    import config from '../config'

    const notifications = ref([])
    const isOpen = ref(false)

    onMounted(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(config.API_HOST + "/api/v1/public/boradcast", {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .configureLogging(LogLevel.Information)
            .build()

        connection.start().then(res => {
            console.info("connection to hub is successful")
        })

        connection.on('NotificationCenter', (notification) => {
            notifications.value.push(notification)
        })
    })

    function unreadNotificationCount() {
        const uneadNotifications = notifications.value.filter(notification => !notification.isRead)
        
        return uneadNotifications.length;
    }
</script>

<script>
    import NotificationList from './NotificationList.vue'

    export default {
        name: 'Notification',
        components: {
            NotificationList,
        }
    }

</script>

<style scoped>
    .notification-item {
        top: 0;
        right:0; 
        bottom: 0;
        position:absolute;
        margin-top: 20px;
        margin-right: 15%;
    }
    .icon-button {
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        background: transparent;
        border: none;
        outline: none;
    }

    .icon-button:hover {
        cursor: pointer;
    }

    .icon-button:active {
        background: #cccccc;
    }

    .icon-button__badge {
        position: absolute;
        top: -10px;
        right: -10px;
        width: 25px;
        height: 25px;
        background: red;
        color: #ffffff;
        display: flex;
        justify-content: center;
        align-items: center;
        border-radius: 50%;
    }

    @media screen and (max-width: 990px) {
        .notification-item {
            margin-right: 30%;
            color: red;
        }
    }
</style>