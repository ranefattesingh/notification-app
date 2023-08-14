<template>
    <div class="notification-container" v-bind:class="{'show-notification-container': isOpen && notifications.length > 0}">
        <ul class="notification">
            <li 
                v-for="notification in notifications" 
                class="notification notification-item"
                v-bind:class="{'read-notification': notification.isRead}"
                @click="handleOnNotificationClick(notification.id)"
                :key="notification.id"
            >
                <a>{{ notification.title }}</a>
            </li>
        </ul>
    </div>
</template>

<script>
    import { toast } from 'vue3-toastify';
    import 'vue3-toastify/dist/index.css';

  export default {
        name: 'NotificationList',
        props: {
            notifications: Array,
            isOpen: Boolean
        },
        methods: {
            handleOnNotificationClick: function (id) {
                this.notifications.forEach(notification => {
                    if (notification.id === id) {
                        toast(notification.description, {
                            autoClose: 5000,
                        });
                        notification.isRead = true
                    }
                })
            },
        },
    }
</script>

<style scoped>
    .notification {
        list-style-type: none;
        margin: 0;
        padding: 0;
        max-height: 220px;
        overflow: auto;
        display: block;

        display: block;
        cursor: pointer;
        font-style: normal;
        font-weight: 400;
        font-variant: normal;
        color: #777;
        width: 100%;
        line-height: 25px;
        background-color: #E9EFF2;
        border-bottom: 1px solid #ddd;

        background: #E9EFF2;
        border-radius: 5px;

        /* hide scrollbars on UI*/
        scrollbar-width: none;
        -ms-overflow-style: none;
        -ms-overflow-style: none;
        
    }

    .notification-item {
        /* border-top: 2px solid #ddd; */
        text-align: center;
        border-bottom: 2px solid #ddd;
        border-radius: 0;
        box-shadow: 0 3px 8px #222;
        padding: 10px 0;
    }

    .notification::-webkit-scrollbar {
        width: 0 !important;
        display: none;
        overflow: -moz-scrollbars-none
    }

    .notification-container::before {
        content: '';
        display: block;
        position: absolute;
        width: 0;
        height: 0;
        color: transparent;
        border: 6px solid #000;
        border-color: transparent transparent #fff;
        right: 0;
        top: 0;
        margin-right: 51%;
        margin-top: -14px;
    }

    .notification-container {
        display: none;
        position: absolute;
        right: 0;
        width: 240px;
        border-top: 2px solid #ddd;
        border-bottom: 2px solid #ddd;
        margin-right: 6.5%;
        box-shadow: 0 3px 8px #222;
        border-radius: 5px;
    }

    .show-notification-container {
        display: block;
    }

    .read-notification {
        background-color: #fff;
    }

    @media screen and (max-width: 990px) {
        .notification-container::before {
            margin-right: 49.4%;
        }

        .notification-container {
            margin-right: 18.5%;
        }
    }
</style>