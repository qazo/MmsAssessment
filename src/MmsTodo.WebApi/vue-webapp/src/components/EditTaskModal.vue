<script setup lang="ts">
import { PropType, inject, onMounted, onUnmounted, reactive, ref, watch } from 'vue'
import { UpdateTaskDto, TaskDto } from '../models';
import { AxiosInstance } from 'axios';
import { Modal } from 'bootstrap';

const emit = defineEmits({
	taskUpdated(payload: TaskDto) { },
	modalHidden() { },
});
const props = defineProps({
	task: {
		type: Object as PropType<TaskDto>,
		required: true,
	},
	isVisible: {
		type: Boolean,
		required: true,
	},
});

const apiClient = inject('http-client') as AxiosInstance;
const formRef = ref<HTMLFormElement>();
const modalRef = ref<HTMLDivElement>();
const modal = ref<Modal>();


function handleSubmit(event: Event) {
	event.preventDefault();
	const requestBody = props.task as UpdateTaskDto;
	apiClient.put('Tasks', requestBody)
		.then(resp => {
			if (!resp.data.isSuccessful) {
				alert(resp.data.message);
				return;
			}
			const updatedTask = resp.data.value as TaskDto;
			emit('taskUpdated', updatedTask);
			hideModal();
		});
}

function hideModal() {
	formRef.value?.reset();
	modal.value?.hide();
}
function fireHideEvent() {
	emit("modalHidden");
}

onMounted(() => {
	modal.value = new Modal(modalRef.value as Element);
	if (props.isVisible) {
		modal.value.show();
	}
	modalRef.value?.addEventListener('hide.bs.modal', fireHideEvent);
});

onUnmounted(() => {
	modalRef.value?.removeEventListener('hide.bs.modal', fireHideEvent);
})

watch(() => props.isVisible, (newValue, oldValue) => {
	if (newValue === oldValue) {
		return;
	}
	if (newValue) {
		modal.value?.show();
	} else {
		modal.value?.hide();
	}
});

</script>

<template>
	<div ref="modalRef" class="modal" tabindex="-1">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-header">
					<h6 class="modal-title">Edit Task</h6>
				</div>
				<div class="modal-body">
					<form ref="formRef" v-if="props.isVisible">
						<div class="mb-3">
							<label for="title" class="col-form-label">Title</label>
							<input required v-model="props.task.title" type="text" id="title" class="form-control">
						</div>
						<div class="mb-3">
							<label for="description" class="col-form-label">Description</label>
							<textarea v-model="props.task.description" id="description" class="form-control"></textarea>
						</div>
					</form>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" @click="hideModal">Cancel</button>
					<button type="button" class="btn btn-primary" @click="handleSubmit">Update</button>
				</div>
			</div>
		</div>
	</div>
</template>