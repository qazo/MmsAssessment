<script setup lang="ts">
import { reactive } from 'vue';
import TaskListItem from './TaskListItem.vue';
import { TaskDto } from '../models';

const props = defineProps({
	taskList: {
		type: Array,
		required: true
	}
});

function onTaskDeleted(task: TaskDto) {

}

function onTaskUpdated(task: TaskDto) {

}

const taskList = reactive(props.taskList as TaskDto[])
</script>

<template>
	<ul class="list-group">
		<template v-for="task in taskList">
			<TaskListItem :task="task" @task-deleted="onTaskDeleted" @task-updated="onTaskUpdated" />
		</template>
	</ul>
</template>