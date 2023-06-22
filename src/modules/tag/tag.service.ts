import {
    BadRequestException,
    Injectable
} from "@nestjs/common";
import {
    CreateTagArgs,
    DeleteTagArgs,
    UpdateTagArgs
} from "./types";
import { TagRepository } from "./tag.repository";
import { plainToClass } from "class-transformer";
import { Tag } from "./models/tag";

@Injectable()
export class TagService {
    constructor(private tagRepository: TagRepository) {}

    async createTag(createTagArgs: CreateTagArgs): Promise<Tag> {
        const { name, userId, description } = createTagArgs;
        const result = await this.tagRepository.createTag({ name, userId, description });
        return plainToClass(Tag, result);
    }

    async deleteTag(deleteTagArgs: DeleteTagArgs) {
        const { tagId } = deleteTagArgs;
        const foundTag = await this.tagRepository.getTag({ tagId });
        if (!foundTag) {
            throw new BadRequestException(`Тег с идентификатором ${ tagId } не найден`);
        }
        await this.tagRepository.removeTag({ tagId });
        const result = await this.tagRepository.getTag({ tagId });
        return plainToClass(Tag, result);
    }

    async updateTag(updateTagArgs: UpdateTagArgs) {
        const { tagId, description, userId, name } = updateTagArgs;
        const foundTag = await this.tagRepository.getTag({ tagId });
        if (!foundTag) {
            throw new BadRequestException(`Комментарий с идентификатором ${ tagId } не найдена`);
        }
        await this.tagRepository.updateTag({ tagId, description, userId, name });
        const result = await this.tagRepository.getTag({ tagId });
        return plainToClass(Tag, result);
    }

    async getTags() {
        const result = await this.tagRepository.getTags();
        return result.map(tag => plainToClass(Tag, tag));
    }
}
