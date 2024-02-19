<script setup lang="ts">
import { PropType, ref } from 'vue';
import TaskListItem from './TaskListItem.vue';
import { TaskDto } from '../models';
import EditTaskModal from './EditTaskModal.vue';

const props = defineProps({
	taskList: {
		type: Array as PropType<TaskDto[]>,
		required: true
	}
});

const emit = defineEmits({
	taskEditRequested(task: TaskDto) { },
})

const isEditVisible = ref(false);
const currentEditTask = ref<TaskDto | null>(null);
function onTaskDeleted(task: TaskDto) {
	for (let i = taskList.length - 1; i >= 0; i--) {
		const elem = taskList[i];
		if (elem.id != task.id) {
			continue;
		}
		taskList.splice(i, 1);
	}
}

function onTaskUpdated(task: TaskDto) {

}

function onTaskEditRequested(task: TaskDto) {
	currentEditTask.value = task;
	isEditVisible.value = true;
}

const taskList = props.taskList;
</script>

<template>
	<ul class="list-group">
		<template v-for="task in taskList" :key="task.id">
			<TaskListItem :task="task" @task-deleted="onTaskDeleted" @task-edit-requested="onTaskEditRequested" />
		</template>
	</ul>

	<template v-if="currentEditTask != null">
		<EditTaskModal :is-visible="isEditVisible" :task="currentEditTask" @task-updated="onTaskUpdated"
			@modal-hidden="() => isEditVisible = false" />
	</template>
</template>