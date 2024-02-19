<script setup lang="ts">
import { inject, onMounted, onUnmounted, reactive, ref, watch } from 'vue'
import { CreateTaskDto, TaskDto } from '../models';
import { Modal } from 'bootstrap'
import { AxiosInstance } from 'axios';

const emit = defineEmits({
	taskCreated(payload: TaskDto) { },
	modalHidden() { },
});

const props = defineProps({
	isVisible: {
		type: Boolean,
		required: true
	}
});

const formRef = ref<HTMLFormElement>();
const model = reactive<CreateTaskDto>({
	title: '',
	description: null
});

const apiClient = inject('http-client') as AxiosInstance;
function handleSubmit(event: Event) {
	event.preventDefault();

	console.log({ model });
	apiClient.post('Tasks', model)
		.then((resp) => {
			const result = resp.data;
			if (!result.isSuccessful) {
				alert(result.message);
				return;
			}
			emit("taskCreated", result.value);
		});
	hideModal();
}

function hideModal() {
	formRef.value?.reset();
	modal.value?.hide();
}

function fireHideEvent() {
	emit("modalHidden");
}

const modalRef = ref<HTMLDivElement>();
const modal = ref<Modal>();

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
	<div ref="modalRef" id="createTaskModal" class="modal" tabindex="-1">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-header">
					<div class="text-center">
						<h5 class="modal-title">Create Task</h5>
					</div>
				</div>
				<div class="modal-body">
					<form ref="formRef">
						<div class="mb-3">
							<label for="title" class="col-form-label">Title</label>
							<input required v-model="model.title" type="text" id="title" class="form-control">
						</div>
						<div class="mb-3">
							<label for="description" class="col-form-label">Description</label>
							<textarea v-model="model.description" id="description" class="form-control"></textarea>
						</div>
					</form>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" @click="hideModal">Cancel</button>
					<button type="button" class="btn btn-primary" @click="handleSubmit">Create</button>
				</div>
			</div>
		</div>
	</div>
</template>