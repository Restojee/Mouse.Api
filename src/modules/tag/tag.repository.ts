import {
    Inject,
    Injectable
} from "@nestjs/common";
import { Repository } from "sequelize-typescript";
import { TagEntity } from "../../database/entities/tag.entity";
import {
    CreateTagArgs,
    GetTagArgs,
    RemoveTagArgs,
    UpdateTagArgs
} from "./types";

@Injectable()
export class TagRepository {
    constructor(
        @Inject('TAGS_REPOSITORY')
        private repository: Repository<TagEntity>
    ) {}

    async createTag(createTagArgs: CreateTagArgs): Promise<TagEntity> {
        const { name, description, userId } = createTagArgs;
        return await this.repository.create({ name, description, userId })
    }

    async updateTag(updateTagArgs: UpdateTagArgs): Promise<TagEntity> {
        const { tagId, userId, name, description } = updateTagArgs;
        await this.repository.update({ userId, name, description }, { where: { id: tagId } });
        return await this.repository.findByPk(updateTagArgs.tagId);
    }

    async getTag(getTagArgs: GetTagArgs): Promise<TagEntity> {
        const { tagId } = getTagArgs;
        return await this.repository.findByPk(tagId);
    }

    async getTags(): Promise<Array<TagEntity>> {
        return await this.repository.findAll();
    }

    async removeTag(removeTagArgs: RemoveTagArgs): Promise<void> {
        const { tagId } = removeTagArgs;
        await this.repository.destroy({ where: { id: tagId } });
    }
}
